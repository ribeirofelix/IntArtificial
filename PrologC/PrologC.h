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

			 return res;
 
		}
		
		// X,Y,P,PERFUME,SCREAMS,SCREAMT,POKEMON
		static void updatePercp(PlEngine e , int x , int y , char * pokeName , bool hasPerfum , bool hasScremS , bool hasScreamT , bool hasPoke )
		{
			if(hasPerfum)
			{
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerfum",av);
			}
			
			{// Always call update trainer perceptions
				PlTermv av(3);
				av[0] = x ;
				av[1] = y ;
				av[2] = hasScreamT ;
				PlCall("updPerScremT",av);
			}
			
			if(hasScremS)
			{
				PlTermv av(2);
				av[0] = x ;
				av[1] = y ;
				PlCall("updPerScremS",av);
			}

			if(hasPoke)
			{
				PlTermv av(3);
				av[0] = x ;
				av[1] = y ;
				av[2] = pokeName;
				PlCall("updPokemon",av);
			}
		
		}


		// Assertions rules!

		static void putMart(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putMart",av);
		}
		static void putPokeCenter(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putPokeCenter",av);
		}
		static void putTrainer(int x,int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("putTrainer",av);
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
			char args[3][100] ; int argIx= 0;
			int * ret = (int*)malloc(sizeof(int)*5);
			int retIdx = 0;


			int i;
			// we must read to the first ( or '\0'
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
			int sizeRellc ;
			switch (predicate[0])
			{
			case 'm': ret[retIdx] = Move ; sizeRellc = 4  ; break;
			case 'l':  ret[retIdx] = Launch ; sizeRellc = 3 ; break ;
			case 't' :
				{
					BestMove turn ;
					if(predicate[4] == 'L')
						turn = TurnLeft;
					else
						turn = TurnRight;
					ret[retIdx] = turn ;   
					sizeRellc = 1;
					break;
				}
			case 'b' : 
				{
					BestMove doWat ;
					if(predicate[1] == 'a')
					{
						doWat = Battle;
						sizeRellc = 5;
					}
					else
					{
						doWat = Buy;
						sizeRellc = 4 ;				
					}
					ret[retIdx] = doWat ;   
					
					break;
				}
			case 'h' : ret[retIdx] = Move ; sizeRellc = 3  ; break;
			default:
				printf("predicado inesperado!"); exit(1);
				break;
			}
			//ret = (int*) realloc( ret , sizeof(int)*sizeRellc ) ; 
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
