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

	private:
		static void * eng;
	

	};
}
