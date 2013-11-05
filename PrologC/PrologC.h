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
				av[2] = PlCompound( pokeName);
				PlCall("updPokemon",av);
			}
		
		}

		static bool isVisited(int x , int y )
		{
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("visited",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}


		static bool isSafe(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("safe",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		static void safes()
		{
			PlTermv av(2);

			PlQuery q("safe",av);

			System::Console::WriteLine("----safes----");
			while( q.next_solution() )
			{
				System::Console::WriteLine("{0},{1}",(int)av[0],(int)av[1]);
			}
			System::Console::WriteLine("----END safes----");
			
		}

		static bool hurtPokemon()
		{
			PlTermv av(1);
			
			PlQuery q("isHurt" ,av);
			q.next_solution();
			return ( (int) av[0] ) == 1;
			
		}
		
		static void trainers()
		{
			PlTermv av(2);

			PlQuery q("trainer",av);

			System::Console::WriteLine("----trainers----");
			while( q.next_solution() )
			{
				System::Console::WriteLine("{0},{1}",(int)av[0],(int)av[1]);
			}
			System::Console::WriteLine("----END trainers----");
			
		}
				
		static void pokemons()
		{
			PlTermv av(3);

			PlQuery q("pokemon",av);

			System::Console::WriteLine("----pokemons----");
			while( q.next_solution() )
			{
				char * poke = (char *) av[2];
				
				System::Console::WriteLine("{0},{1},{2}{3}{4}",(int)av[0],(int)av[1], poke[0],poke[1],poke[2] );
			}
			System::Console::WriteLine("----END pokemons----");
			
		}
		
		static void screamsT()
		{
			PlTermv av(2);

			PlQuery q("screamTrainer",av);

			System::Console::WriteLine("----screams gary----");
			while( q.next_solution() )
			{
				System::Console::WriteLine("{0},{1}",(int)av[0],(int)av[1] );
			}
			System::Console::WriteLine("----END screams gary----");
		}

		static int pokeballs()
		{
			PlTermv av(1);

			PlQuery q("pokeball",av);
			q.next_solution();

			return (int)av[0];

		}

		/********************/
		/* Assertions rules */
		/********************/
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
		
		static void updFacing(char * direction)
		{
			PlTermv av(1);
			av[0] = PlCompound(direction);
			PlCall("updFacing",av);
		}
		
		static void removeSafe(int x, int y)
		{
			PlTermv av(2);
			av[0] = x ;
			av[1] = y;
			PlCall("removeSafe",av);

		}

		/**************************/
		/* End - Assertions rules */
		/**************************/


		static void removeMart(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("rmvMart",av);
		}
		static void removePokeCenter(int x, int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("rmvPokeCenter",av);
		}
		static void removeTrainer(int x,int y)
		{
			PlTermv av(2);
			av[0] = x  ;
			av[1] = y ;
			PlCall("rmvTrainer",av);
		}


		
		static bool isMart(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("mart",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		
		static bool isPokeCenter(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("pokeCenter",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
		}

		
		static bool isTrainer(int x, int y)
		{
			
			PlTermv av(2);
			av[0] = x ;
			av[1] = y ;
			PlQuery q("trainer",av);
			bool resp = q.next_solution() == 1 ;
			
			return resp;
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
			switch (predicate[0])
			{
			case 'm': ret[retIdx] = Move ; break;
			case 'l':  ret[retIdx] = Launch ; break ;
			case 't' :
				{
					BestMove turn ;
					if(predicate[4] == 'L')
						turn = TurnLeft;
					else
						turn = TurnRight;
					ret[retIdx] = turn ;   
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
					
					break;
				}
			case 'h' : ret[retIdx] = Heal ; break;
			case 'a': ret[retIdx] = AStar ;  break;
			case 'j' : ret[retIdx] = Joker ; break;
			case 'k' : ret[retIdx] = KillGary ; break ;
			case 'g' : ret[retIdx] = GoPokeCenter ; break ;
			case 'c': ret[retIdx] = CatchPokemon; break;
			default:
				printf("predicado inesperado!"); exit(1);
				break;
			}
			retIdx++;

			for (int i = 0; i <= argIx; i++ , retIdx++)
			{
				int numArg = (int) strtol(args[i],NULL,0);
				if(numArg == 0 &&  strcmp( args[i] , "0" ) != 0  ) // then is a string as value, a pokemon!
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
			AStar ,
			TurnBack,
		    Joker ,
			KillGary ,
			GoPokeCenter ,
			CatchPokemon ,

		};
	};

}
