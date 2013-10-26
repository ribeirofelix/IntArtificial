// PrologC.h

#include <SWI-cpp.h>
#include <iostream>

namespace PrologC {

	class Prolog
	{
	public:
	
		static PlEngine initilize(char * path )
		{
			char* argv[] = {"swipl.dll", "-s", path , 0}; 
			_putenv("SWI_HOME_DIR=C:\\Program Files (x86)\\swipl"); 
 
			PlEngine eng(3,argv);
			return eng;
		}

		static int* bestMove( PlEngine e)
		{
			 PlTermv av(1); 
			
			 PlQuery q("bestMove", av); 

			 q.next_solution();
			 char * ret =  ( char*)av[0];
			int * res   = parseBestMove(ret);


			/* 
			 PlTermv av2(3);

			
			 PlQuery q2("groundType", av2); 

			 while(q2.next_solution())
			 {
				 char * x = (char*)av2[0];
				 char * y = (char*)av2[1];
				 char * t = (char *) av2[2];
				 printf("%s %s",x,y);
			 }
*/
			 return res;
 
		}
		// X,Y,P,PERFUME,SCREAMS,SCREAMT,POKEMON
		static void updatePercp(PlEngine e , int x , int y , char * pokeName , bool hasPerfum , bool hasScremS , bool hasScreamT , bool hasPoke )
		{
			PlTermv av(7);
			char buffer [2][10];
		
			av[0] = x ;
			av[1] = y ;
			av[2] = pokeName ;
			av[3] = hasPerfum ;
			av[4] = hasScremS ;
			av[5] = hasScreamT ;
			av[6] = hasPoke ;

			PlCall("upPerc",av);

		
		}

		static void assert(char * predicate)
		{
			PlTermv av(1);
			av[0] = predicate  ;
			PlCall("assert",av);
		}


		static void putGround(int x, int y, char t )
		{
			PlTermv av(3);
			av[0] = x  ;
			av[1] = y ;
			av[2] = t ;
			PlCall("putGround",av);
		}

	private:


		static int * parseBestMove(char * answ)
		{
			char predicate[100] ; 
			char args[2][100] ; int argIx= 0;
			int * ret = (int*)malloc(sizeof(int));
			int retIdx = 0;


			int i;
			// we must read to the first ( 
			for ( i = 0; !(answ[i] == '(' || answ[i] == '\0') ; i++)
			{
				predicate[i] = answ[i];
			}
			
			int j = 0;
			for (i ++ ; answ[i] != '\0'; i++)
			{
				if(answ[i] == ',' ||answ[i] == ')' )
				{
					args[argIx][j] = '\0';
					if(answ[i] == ')')
						break;
					else
					{						
						argIx++; j = 0 ;
					}
					
				}
				else
				{
					args[argIx][j] = answ[i];
					j++;
				}
			}

			// Predicate cases.
			switch (predicate[0])
			{
			case 'm': ret[retIdx] = Move ; ret = (int*) realloc( ret , sizeof(int)*(retIdx + 3) ) ; break;
			case 'l':  ret[retIdx] = Launch ; ret = (int*) realloc( ret , sizeof(int)*(retIdx + 2) ) ; break ;
			case 't' :
				{
					BestMove turn ;
					if(predicate[4] == 'l')
						turn = TurnLeft;
					else
						turn = TurnRight;
					ret[retIdx] = turn ;   
					ret = (int*) realloc( ret , sizeof(int)*(retIdx + 3) ) ; 
					break;
				}
			case 'b' : 
				{
					BestMove doWat ;
					if(predicate[1] == 'a')
						doWat = Battle;
					else
						doWat = Buy;
					ret[retIdx] = doWat ;   
					ret = (int*) realloc( ret , sizeof(int)*(retIdx + 3) ) ; 
					break;
				}
			case 'h' : ret[retIdx] = Move ; ret = (int*) realloc( ret , sizeof(int)*(retIdx + 3)) ; break;
			default:
				printf("predicado inesperado!"); exit(1);
				break;
			}
			retIdx++;

			for (int i = 0; i <= argIx; i++ , retIdx++)
			{
				int numArg = (int) strtol(args[i],NULL,0);
				if(numArg == 0 ) // then is a string as value, a pokemon!
					ret[retIdx] = -1;
				else
					ret[retIdx] = numArg;

			}
			
			ret[retIdx] = -1 ;
			
			return ret;
			

		}
	
		enum BestMove
		{
			Launch,
			Heal,
			Buy,
			Battle,
			Move,
			TurnRight,
			TurnLeft,

		};
	};

}