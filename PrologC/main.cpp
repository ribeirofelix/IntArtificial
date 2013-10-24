#include <SWI-cpp.h> 
#include <iostream> 
 
using namespace std; 
 
int main(){ 
 char* argv[] = {"swipl.dll", "-s", "E:\\Documentos\\PUC-Rio_Trabalhos\\IntArtificial\\Prolog\\rules.pl", NULL}; 
 _putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl"); 
 
 PlEngine e(3,argv); 
 
// PlTermv av(2); 
// av[1] = PlCompound("jose"); 
// PlQuery q("ancestral", av); 

 PlTermv av(1); 
 //av[1] = PlCompound(); 

 PlQuery q("bestMove", av); 
 
 while (q.next_solution()) 
 {
	 printf("%s\n", (char*)av[0] );

 } 
 
 cin.get(); 
 return 1; 
} 