// PrologC.h

#include <SWI-cpp.h>
#include <iostream>

namespace PrologC {

	class Prolog
	{
	public:
		

		static int* bestMove(char * prologFile)
		{
				char* argv[] = {"swipl.dll", "-s", prologFile , 0}; 
			_putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl"); 
 
			PlEngine e(3,argv); 

			 PlTermv av(1); 
			
			 PlQuery q("bestMove", av); 

			 q.next_solution();
			 char * ret =  ( char*)av[0];
			 int * res  =  (int*) malloc(sizeof(int)*3 ) ;
			 res[0] = 1;
			 res[1] = 25;
			 res[2] = 19;
			 return res;
 
		}

	};

}
