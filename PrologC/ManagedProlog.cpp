#include "stdafx.h"
#include "PrologC.h"


#pragma once
using namespace System;
namespace ManagedProlog {
	public ref class Prolog
	{
	public:
		Prolog(){}


		static void Initilize(char * path)
		{
			PlEngine e = PrologC::Prolog::initilize(path);
			eng = (void*) &e;
		}
		
		static int * BestMove()
		{
			PlEngine * e = (PlEngine *) eng;
			return  PrologC::Prolog::bestMove( *e ) ;
		}

		static void UpdPerc(int x , int y , char * pokeName , bool hasPerfum , bool hasScremS , bool hasScreamT , bool hasPoke)
		{
			PlEngine * e = (PlEngine *) eng;
			PrologC::Prolog::updatePercp(*e,x,y,pokeName,hasPerfum,hasScremS,hasScreamT,hasPoke);
		}

		static bool IsVisited(int x , int y )
		{
			return PrologC::Prolog::isVisited(x,y);
		}

		static bool IsSafe(int x, int y)
		{
			return PrologC::Prolog::isSafe(x,y);
		}

		static void PutGround(int x, int y, char t )
		{			
			PrologC::Prolog::putGround(x,y,t);
		}
		static void PutMart(int x, int y )
		{			
			PrologC::Prolog::putMart(x,y);
		}
		static void PutPokeCenter(int x, int y)
		{			
			PrologC::Prolog::putPokeCenter(x,y);
		}
		static void PutTrainer(int x, int y )
		{			
			PrologC::Prolog::putTrainer(x,y);
		}

		static void RemoveMart(int x, int y )
		{			
			PrologC::Prolog::removeMart(x,y);
		}
		static void RemovePokeCenter(int x, int y)
		{			
			PrologC::Prolog::removePokeCenter(x,y);
		}
		static void RemoveTrainer(int x, int y )
		{			
			PrologC::Prolog::removeTrainer(x,y);
		}

		static bool IsMart(int x, int y)
		{
			return PrologC::Prolog::isMart(x,y);
		}

		
		static bool IsPokeCenter(int x, int y)
		{
			return PrologC::Prolog::isPokeCenter(x,y);
		}

		
		static bool IsTrainer(int x, int y)
		{
			return PrologC::Prolog::isTrainer(x,y);
		}

		static void UpdFacing(char * direction)
		{
			PrologC::Prolog::updFacing(direction);
		}

		static void RemoveSafe(int x,int y)
		{
			PrologC::Prolog::removeSafe(x,y);
		}
		
		static void Safes()
		{
			PrologC::Prolog::safes();
		}

		static bool IsHurt()
		{
			return PrologC::Prolog::hurtPokemon();
		}

		static void Trainers()
		{
			PrologC::Prolog::trainers();
		}

		static void Pokemons()
		{
			PrologC::Prolog::pokemons();
		}

		static void ScreamsT()
		{
			PrologC::Prolog::screamsT();
		}

		static int Pokeballs()
		{
			return PrologC::Prolog::pokeballs();
		}
	private:
		static void * eng;
	

	};
}
