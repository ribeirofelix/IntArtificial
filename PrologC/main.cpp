#include <SWI-cpp.h> 
#include <iostream> 
 
using namespace std; 

char * bestMove();

void updPerc(const char * x , const char *  y , const char *  hasPefm , const char *  hasScreams, const char *  hasScreamt);
 
int main(){ 
	 char* argv[] = {"swipl.dll", "-s", "E:\\Documentos\\PUC-Rio_Trabalhos\\IntArtificial\\Prolog\\rules.pl", NULL}; 
	 _putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl"); 
 
	 PlEngine e(3,argv); 
 
	// PlTermv av(2); 
	// av[1] = PlCompound("jose"); 
	// PlQuery q("ancestral", av); 

	
 
	 printf("%s\n", bestMove() );

	updPerc("25","19","1","0","0");

	// printf("%s\n", bestMove() );
	int i = 0;
	 while (i<10)
	 {
		  printf("%s\n", bestMove() );
		  i++;
	 } 
 
	 cin.get(); 
	 return 1; 
} 

char * bestMove()
{
	 PlTermv av(1); 
	 //av[1] = PlCompound(); 

	 PlQuery q("bestMove", av); 

	 q.next_solution();
 
	 return (char*)av[0];
	
}


void updPerc(const char * x , const char *  y , const char *  hasPefm , const char *  hasScreams, const char *  hasScreamt)
{
	 PlTermv av(5); 
	 //av[1] = PlCompound();
	 //X,Y,PERFUME,SCREAMS,SCREAMT
	 av[0] = PlCompound( x ); 
	 av[1] = PlCompound( y ); 
	 av[2] = PlCompound( hasPefm ); 
	 av[3] = PlCompound( hasScreams ); 
	 av[4] = PlCompound( hasScreamt ); 
	 
	 PlQuery q("upPerc", av); 

	 q.next_solution();
}