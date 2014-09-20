using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    /// <summary>
    /// Classe que prove as operações de ordenação com Heap.
    /// </summary>
    /// <typeparam name="TValue">Tipo genérico dos elementos que compõem a lista de prioridades</typeparam>
    public class Heap<TValue>
    {
        /// <summary>
        /// Mantem um heap de elementos genéricos:
        /// Tupla: Item1 - Chave ou prioridade do elemento no Heap; Item2 - Índice identificador do elemento no Heap; Item3 - Conteúdo (genérico) do elemento.
        /// </summary>
        private List<Tuple<int, int, TValue>> heapTree;

        /// <summary>
        /// Vetor que guarda a posição de cada elemento do Heap. Usa o índice do elemento para gurdar sua posição.
        /// </summary>
        private Dictionary<int, int> heapPosition;

        /// <summary>
        /// Inicializa um Heap vazio.
        /// </summary>
        public Heap()
        {
            //Apenas inicializa os atributos da classe Heap.
            heapTree = new List<Tuple<int, int, TValue>>();
            heapPosition = new Dictionary<int, int>();
        }

        /// <summary>
        /// Construtor da classe Heap. Inicializa um Heap Min a partir de uma lista de prioridades. Inicializa as posições dos elementos no Heap.
        /// </summary>
        /// <param name="priorityQueue">Lista de elementos com suas prioridades</param>
        public Heap(List<Tuple<int, int, TValue>> priorityQueue)
        {
            //Guarda os valores no atributo heapTree para depois ordenar como um heap min
            heapTree = priorityQueue;

            //Inicializa as posições dos elementos do heap
            //heapTree.Count é um atributo da classe List que guarda o número de elementos da lista
            heapPosition = Enumerable.Range(0, heapTree.Count).ToDictionary<int, int>(a => a + 1);

            //Executa o heapfyDownMin para cada elemento a partir da metade da lista até o primeiro
            //Cria o heap em O(n)
            //heapTree.Count é um atributo da classe List que guarda o número de elementos da lista
            for (int i = (int)Math.Ceiling(heapTree.Count / 2.0); i >= 0; i--)
            {
                HeapfyDownMin(i);
            }
        }

        /// <summary>
        /// Ordena o vetor de entrada usando o heap sort.
        /// </summary>
        /// <param name="priorityQueue">Lista de elementos com suas prioridades</param>
        public static void HeapSort(ref List<Tuple<int, TValue>> priorityQueue)
        {
            //Coloca o vetor na ordem heap (Max).
            HeapfyMax(ref priorityQueue);

            //Recupera o tamanho do vetor
            int size = priorityQueue.Count;

            //Sucessivamente coloca o maior elemento no final do vetor
            for (int i = size - 1; i >= 0; i--)
            {
                //Troca o primeiro elemento com o último
                SwapElements(ref priorityQueue, 0, i);
                //Diminui o tamenho do vetor
                size--;
                //Restaura a propriedade do heap
                HeapfyDownMax(ref priorityQueue, size, 0);
            }
        }

        /// <summary>
        /// Recebe uma lista de prioridades reordena a mesma na forma de Heap Max in place em O(n).
        /// </summary>
        /// <param name="priorityQueue">Lista de prioridades</param>
        private static void HeapfyMax(ref List<Tuple<int, TValue>> priorityQueue)
        {
            //Executa o heapfyDown para cada elemento a partir da metade da lista até o primeiro
            //Cria o heap em O(n)
            //priorityQueue.Count é um atributo da classe List que guarda o número de elementos da lista
            for (int i = (int)Math.Ceiling(priorityQueue.Count / 2.0); i >= 0; i--)
            {
                HeapfyDownMax(ref priorityQueue, priorityQueue.Count, i);
            }
        }

        /// <summary>
        /// Compara a prioridade de um elemento do Heap Max com seus filhos e troca posições quando necessário.
        /// Faz isso recusrsivamente até que o elemento esteja na posição correta no Heap Max, ou seja, sua prioridade é maior do que a dos filhos.
        /// </summary>
        /// <param name="priorityQueue">Lista de prioridades de onde o elemento faz parte</param>
        /// <param name="heapSize">Tamanho do heap</param>
        /// <param name="elementPosition">Posição do elemento na lista de prioridades</param>
        /// <returns></returns>
        private static void HeapfyDownMax(ref List<Tuple<int, TValue>> priorityQueue, int heapSize, int elementPosition)
        {
            //Variaveis auxiliares para representar os filhos
            int leftChild = elementPosition * 2 + 1;
            int rightChild = elementPosition * 2 + 2;
            //Variavel que será usada para guardar o maior entre elemento atual, filho da direita e filho da esquerda
            int biggerChild = 0;

            //Verifica se o filho da esquerda é válido e se é maior do que o elemento atual
            if (leftChild < heapSize && priorityQueue[leftChild].Item1 > priorityQueue[elementPosition].Item1)
                biggerChild = leftChild;
            else
                biggerChild = elementPosition;

            //Verifica se o filho da direita é válido e se é maior do que o maior calculado no passo anterior
            if (rightChild < heapSize && priorityQueue[rightChild].Item1 > priorityQueue[biggerChild].Item1)
                biggerChild = rightChild;

            //Se o elemento não é maior do que o maior dos filhos
            if (biggerChild != elementPosition)
            {
                //Troca os elementos para manter a propriedade do heap entre eles
                SwapElements(ref priorityQueue, biggerChild, elementPosition);
                //Mantem a porpriedade do heap no nívelm ais baixo
                HeapfyDownMax(ref priorityQueue, heapSize, biggerChild);
            }
        }

        /// <summary>
        /// Compara a prioridade de um elemento do Heap Min com seus filhos e troca posições quando necessário.
        /// Faz isso recusrsivamente até que o elemento esteja na posição correta no Heap Min, ou seja, sua prioridade é maior do que a dos filhos.
        /// </summary>
        /// <param name="elementPosition">Posição do elemento na lista de prioridades</param>
        private void HeapfyDownMin(int elementPosition)
        {
            //Variaveis auxiliares para representar os filhos
            int leftChild = elementPosition * 2 + 1;
            int rightChild = elementPosition * 2 + 2;

            //Variavel que será usada para guardar o menor entre elemento atual, filho da direita e filho da esquerda
            int smallerChild = 0;

            //Verifica se o filho da esquerda é válido e se é menor do que o elemento atual
            //priorityQueue.Count é um atributo da classe List que guarda o número de elementos da lista
            if (leftChild < heapTree.Count && heapTree[leftChild].Item1 < heapTree[elementPosition].Item1)
                smallerChild = leftChild;
            else
                smallerChild = elementPosition;

            //Verifica se o filho da direita é válido e se é maior do que o menor calculado no passo anterior
            //priorityQueue.Count é um atributo da classe List que guarda o número de elementos da lista
            if (rightChild < heapTree.Count && heapTree[rightChild].Item1 < heapTree[smallerChild].Item1)
                smallerChild = rightChild;

            //Se o elemento não é menor do que o menor dos filhos
            if (smallerChild != elementPosition)
            {
                //Troca os elementos para manter a propriedade do heap entre eles
                SwapElements(smallerChild, elementPosition);
                //Mantem a porpriedade do heap no nível mais baixo
                HeapfyDownMin(smallerChild);
            }
        }

        /// <summary>
        /// Mantem a propriedade do Heap para cima
        /// </summary>
        /// <param name="elementPosition">Posição do elemento na lista de prioridades</param>
        private void HeapfyUpMin(int elementPosition)
        {
            int fatherPosition = 0;

            //Verifica se a posição do elemento é par ou impar e calcula a posição do pai de acordo
            //Verifica o resto da divisão por 2
            if ((elementPosition % 2 == 0) && (elementPosition != 0))
                //É par
                fatherPosition = (elementPosition - 2) / 2;
            else if (elementPosition != 0)
                //É impar
                fatherPosition = (elementPosition - 1) / 2;

            //Verifica se a prioridade do pai é maior do que a do elemento
            //O item1 da tupla é a prioridade do elemento
            if (heapTree[fatherPosition].Item1 > heapTree[elementPosition].Item1)
            {
                //Se for então troca os elemento para restaurar a propriedade do heap
                SwapElements(elementPosition, fatherPosition);
                //Mantem a propriedade do heap no nível acima
                HeapfyUpMin(fatherPosition);
            }
        }

        /// <summary>
        /// Extrai e retorna o elemento de menor prioridade do Heap Min
        /// </summary>
        public Tuple<int, int, TValue> HeapExtractMin()
        {
            //Salva o primeiro elemento do heap (mínimo)
            var minElement = heapTree[0];

            //Troca o primeiro elemento com o último
            SwapElements(0, heapTree.Count - 1);

            //Diminui o tamanho do heap deletando o útimo elemento
            heapTree.RemoveAt(heapTree.Count - 1);

            //Remove a posição do elemento do vetor que guarda as posições de elementos do Heap
            heapPosition.Remove(minElement.Item2);

            //Restaura a propriedade do heap
            HeapfyDownMin(0);

            return minElement;
        }

        /// <summary>
        /// Altera o valor da chave (prioridade) de um elemento do heap.
        /// </summary>
        /// <param name="newKeyValue">Novo valor da prioridade.</param>
        /// <param name="elementIndex">Índice (ou valor) do elemento.</param>
        public void HeapChangeKey(int newKeyValue, int elementIndex)
        {
            //Antes de tentar alterar o elemento verifica se o mesmo realmente existe.
            if (heapPosition.ContainsKey(elementIndex))
            {
                //Guarda a chave (prioridade) antiga do elemento
                var oldKey = heapTree[heapPosition[elementIndex]].Item1;
                //Altera a prioridade do elemento (e mantem o restante do conteúdo do elemento)
                heapTree[heapPosition[elementIndex]] = new Tuple<int, int, TValue>(newKeyValue, heapTree[heapPosition[elementIndex]].Item2, heapTree[heapPosition[elementIndex]].Item3);

                //Restaura o Heap
                if (newKeyValue > oldKey)
                    HeapfyDownMin(heapPosition[elementIndex]);

                else if (newKeyValue < oldKey)
                    HeapfyUpMin(heapPosition[elementIndex]);
            }
        }

        /// <summary>
        /// Retorna o valor da chave (prioridade) de um elemento do heap.
        /// Se não encontrar o elemento retorna uma valor negativo.
        /// </summary>
        /// <param name="elementIndex">Índice (ou valor) do elemento.</param>
        public int HeapGetKey(int elementIndex)
        {
            if (heapPosition.ContainsKey(elementIndex))
            {
                //Apenas retorna a chave (prioridade) do elemento (item1 da tupla)
                //A posição do elemento na árvore heap está armazenada no atributo heapPosition. O heapPosition está organizado pelo índice (ou valor) dos elemento.
                return heapTree[heapPosition[elementIndex]].Item1;
            }

            //Se não encontrar o elemento retorna uma valor negativo.
            return -1;

        }

        /// <summary>
        /// Troca dois elementos de posição no heap Min mantido pela classe Heap
        /// </summary>
        /// <param name="elementPosition1">Posição do primeiro elemento</param>
        /// <param name="elementPosition2">Posição do segundo elemento</param>
        /// <returns></returns>
        private void SwapElements(int elementPosition1, int elementPosition2)
        {
            var tempElement = heapTree[elementPosition1];

            //Troca conteúdo e posição dos elementos
            heapTree[elementPosition1] = heapTree[elementPosition2];

            if (heapPosition.ContainsKey(heapTree[elementPosition1].Item2))
                heapPosition[heapTree[elementPosition1].Item2] = elementPosition1;

            //Troca conteúdo e posição dos elementos
            heapTree[elementPosition2] = tempElement;

            if (heapPosition.ContainsKey(heapTree[elementPosition2].Item2))
                heapPosition[heapTree[elementPosition2].Item2] = elementPosition2;
        }

        /// <summary>
        /// Troca dois elementos de posição em uma lista de prioridades
        /// </summary>
        /// <param name="priorityQueue">Lista de prioridades</param>
        /// <param name="elementPosition1">Posição do primeiro elemento</param>
        /// <param name="elementPosition2">Posição do segundo elemento</param>
        /// <returns></returns>
        private static void SwapElements(ref List<Tuple<int, TValue>> priorityQueue, int elementPosition1, int elementPosition2)
        {
            var tempElement = priorityQueue[elementPosition1];
            priorityQueue[elementPosition1] = priorityQueue[elementPosition2];
            priorityQueue[elementPosition2] = tempElement;
        }

        /// <summary>
        /// Adiciona um novo elemento no Heap.
        /// </summary>
        /// <param name="elementKey">Chave ou prioridade do elemento.</param>
        /// <param name="elementIndex">Indice identificador do elemento (deve ser único).</param>
        /// <param name="elementContent">Conteúdo (genérico) do elemento.</param>
        public void HeapAdd(int elementKey, TValue elementContent)
        {
            //Formata o novo elemento
            var newElement = new Tuple<int, int, TValue>(elementKey, -1, elementContent);
            //Gurda a posição onde o novo elemento vai ficar no Heap
            int NewElementPosition = heapTree.Count;
            //Adiciona o elemento na arvore Heap (na última posição)
            heapTree.Add(newElement);

            //Restaura o Heap
            HeapfyUpMin(NewElementPosition);
        }

        /// <summary>
        /// Retorna o número de elementos atual do Heap. Somente para o Heap instanciado.
        /// </summary>
        /// <returns></returns>
        public int HeapSize()
        {
            return heapTree.Count;
        }

        #region DEBUG TEST EXPORT
#if DEBUG

        public int HeapPositionSize()
        {
            return (int) heapPosition.Count;
        }

#endif
        #endregion

    }

    /// <summary>
    /// Classe que prove as operações de ordenação diversas.
    /// </summary>
    /// <typeparam name="TValue">Tipo genérico dos elementos que compõem a lista de prioridades</typeparam>
    public static class Sorting<TValue>
    {
        /// <summary>
        /// Recebe uma lista de prioridades e retorna a mesma ordenada.
        /// </summary>
        /// <param name="priorityQueue">Lista de prioridades no formato (prioridade, elemento).</param>
        /// <param name="range">Máxima prioridade de um elemento na lista.</param>
        /// <returns></returns>
        public static List<Tuple<int, TValue>> CountingSort(List<Tuple<int, TValue>> priorityQueue, int range)
        {
            //Vetor auxiliar que será retornado ao final
            List<Tuple<int, TValue>> orderedList = new List<Tuple<int, TValue>>();

            //Legenda:
            //priorityQueue.item1 = Prioridade do elemento na lista
            //priorityQueue.item2 = Valor ou conteúdo do elemento na lista

            //Histograma do Counting Sort que será usado para guardar listas de elementos com o mesmo peso
            List<int>[] histogram = new List<int>[range + 1];

            //Percorre a lista de prioridades e preenche o histograma
            for (int i = 0; i < priorityQueue.Count; i++)
            {
                //O histograma (usando a prioridade do elemento como posição) guarda (na lista daquela prioridade) o índice (posição) do elemento na lista de prioridades
                if (histogram[priorityQueue[i].Item1] == null)
                {
                    histogram[priorityQueue[i].Item1] = new List<int>();
                }

                histogram[priorityQueue[i].Item1].Add(i);
            }

            //Monta a lista ordenada a partir da lista de entrada e do histograma
            foreach (var priority in histogram)
            {
                if (priority == null)
                    continue;

                foreach (var element in priority)
                {
                    //Coloca na lista ordenada o proximo elemento indicado pelo histograma
                    orderedList.Add(priorityQueue[element]);
                }
            }

            return orderedList;
        }
    }
}