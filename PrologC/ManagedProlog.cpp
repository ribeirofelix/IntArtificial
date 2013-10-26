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

	private:
		static void * eng;
	

	};
}
