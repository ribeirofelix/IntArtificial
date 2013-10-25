#include "stdafx.h"
#include "PrologC.h"


#pragma once
using namespace System;
namespace ManagedProlog {
	public ref class Prolog
	{
	public:
		Prolog(){}


		
		
		static int * BestMove(char * path)
		{
			return  PrologC::Prolog::bestMove(path) ;
		}

	

	};
}
