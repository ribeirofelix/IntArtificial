%-----------------------------------
% Dynamic procedures
%-----------------------------------

:- dynamic mart/2.
:- dynamic pokeCenter/2.
:- dynamic trainer/2.
:- dynamic visited/2.
:- dynamic pokemon/3.
:- dynamic perfumeJoy/2.
:- dynamic screamSeller/2.
:- dynamic screamTrainer/2.
:- dynamic facing/1.
:- dynamic at/2.
:- dynamic visited/2.
:- dynamic groundType/3.
:- dynamic flying/0.
:- dynamic fire/0.
:- dynamic water/0.
:- dynamic electric/0.
:- dynamic hurtPokemon/0.
:- dynamic pokeball/1.
:- dynamic safeLst/1 .
:- dynamic pokedex/1.

%-----------------------------------
% End of dynamic procedures
%-----------------------------------

%-----------------------------------
% Lists
%-----------------------------------


addList(X,L,[X|L]).

delList(X,[X|Tail],Tail).
delList(X,[Y|Tail],[Y|Tail1]) :- delList(X,Tail,Tail1).

isPart(X,[X|Tail]).
isPart(X,[Head|Tail]) :- isPart(X,Tail).

removeHead([Head|Tail],Head,Tail).

% rem(V,[V|Tail],Tail) , V=safe(X,Y) , allowed(X,Y).
% rem(V,[Q|Tail],[Q|Tail1]) :- rem(V,Tail,Tail1).

remove(X,Y,[L|LS],L1) :- removeHead([L|LS],L,Tail) , L = safe(X,Y) , allowed(X,Y).

isSafe(H,[H|R]) :- H = safe(X,Y) .
isSafe(H,[Y|R]) :- isSafe(H,R).

isEmpty([]).

%-----------------------------------
% End of Lists
%-----------------------------------

%-----------------------------------
% Some rules
%-----------------------------------

% se o local é safe e ainda nao foi visitado coloca na lista
includeList(X,Y,L,L1) :- (not(visited(X,Y)) , (X > -1  , X < 42) , (Y > -1 , Y < 42 ) , addList(safe(X,Y),L,L1) , retract(safeLst(L)) , assert(safeLst(L1)) ) ; true .

% se o local acabou de ser visitado tira da lista
takeList(X,Y,L,L1) :- delList(safe(X,Y),L,L1) , retract(safeLst(L)) , assert(safeLst(L1)) .


% se ash está em X,Y, então este local foi visitado
% visited(X,Y) :- at(X,Y) .

% se o ash esta em X,Y e não tem trainador ali, ali é seguro.
safe(X,Y) :- safeLst(L) , isSafe(safe(X,Y),L) .


putGround(X,Y,T) :- assert(groundType(X,Y,T)) .
putMart(X,Y) :-  not(mart(X,Y)) , assert(mart(X,Y)) .
putPokeCenter(X,Y) :- not(pokeCenter(X,Y)) , assert(pokeCenter(X,Y)).
putTrainer(X,Y) :- not(trainer(X,Y)) , assert(trainer(X,Y)) , safeLst(L) , takeList(X,Y,L,L1).


rmvMart(X,Y) :- retract(mart(X,Y)) .
rmvPokeCenter(X,Y) :- retract(pokeCenter(X,Y)).
rmvTrainer(X,Y) :- retract(trainer(X,Y)) , not(safe(X,Y)) , safeLst(L) , includeList(X,Y,L,L1).




removeSafe(X,Y) :- safeLst(L) , ( takeList(X,Y,L,L1) ) . 


% verifica compatibilidade de pokemon e terreno
iscomp(G) :-   (G == 71) ;
			   (G == 65 , water);
			   (G == 67 , electric);
			   (G == 77 , flying);
			   (G == 76 , fire)  .

allowed(X,Y) :- groundType(X,Y,G) , iscomp(G) .

setType(P) :- ( type(P,T) , type(P,K) , T \== K , assert(T) , assert(K) ) ; (type(P,K) , assert(K) ).


distance_min(L,MinXY ) :-  at(X,Y) , distance_min(L, X, Y, MinXY).
distance_min(L, X0, Y0, MinXY) :-    aggregate( min(D, [Xt,Yt]) , (member([Xt,Yt], L) , D is sqrt((Xt-X0)^2+(Yt-Y0)^2)), MinXY).

nrstPokeCenter(X,Y) :- setof([Xs,Ys], pokeCenter(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

nrstTrainer(X,Y) :- setof([Xs,Ys], trainer(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

nrstPokemon(X,Y) :- setof([Xs,Ys], pokemon(Xs,Ys),L) , distance_min(L,MinXY) , MinXY = min(D,[X,Y]) .

pokemon(X,Y) :- pokemon(X,Y,_).


nrstAllowed(L,MinXY ) :-  at(X,Y) , nrstAllowed(L, X, Y, MinXY).
nrstAllowed(L, X0, Y0, MinXY) :-    aggregate( min(D, safe(Xt,Yt)) , (member(safe(Xt,Yt), L) , D is sqrt((Xt-X0)^2+(Yt-Y0)^2) , allowed(Xt,Yt) ), MinXY).

isAllowed(H,L) :- nrstAllowed(L,MinSfAlw) , MinSfAlw = min(D,H) .


%-----------------------------------
% End of some rules
%-----------------------------------


% ----------------------------------
% Pokemons
% ----------------------------------



%fairy
type(clefairy,fairy).
type(clefable,fairy).
type(jigglypuff,fairy).
type(wigglytuff,fairy).
type(mr_Mime,fairy).

%steel
type(magnemite,steel).
type(magneton,steel).

%ghost

type(gastly,ghost).
type(haunter,ghost).
type(gengar,ghost).

%ice
type(dewgong,ice).
type(cloyster,ice).
type(jynx,ice).
type(lapras,ice).
type(articuno,ice).

%pground

type(sandshrew,pground).
type(sandslash,pground).
type(nidoqueen,pground).
type(nidoking,pground).
type(diglett,pground).
type(dugtrio,pground).
type(geodude,pground).
type(graveler,pground).
type(golem,pground).
type(onix,pground).
type(cubone,pground).
type(marowak,pground).
type(rhyhorn,pground).
type(rhydon,pground).


%fighting

type(mankey,fighting).
type(primeape,fighting).
type(poliwrath,fighting).
type(machop,fighting).
type(machoke,fighting).
type(machamp,fighting).
type(hitmonlee,fighting).
type(hitmonchan,fighting).

%dragon
type(dratini,dragon).
type(dragonair,dragon).
type(dragonite,dragon).


%psychic

type(abra,psychic).
type(kadabra,psychic).
type(alakazam,psychic).
type(slowpoke,psychic).
type(slowbro,psychic).
type(drowzee,psychic).
type(hypno,psychic).
type(exeggcute,psychic).
type(exeggutor,psychic).
type(starmie,psychic).
type(mr_Mime,psychic).
type(jynx,psychic).
type(mewtwo,psychic).

%electric

type(pikachu,electric).
type(raichu,electric).
type(magnemite,electric).
type(magneton,electric).
type(voltorb,electric).
type(electrode,electric).
type(electabuzz,electric).
type(jolteon,electric).
type(zapdos,electric).


%bug

type(caterpie,bug).
type(metapod,bug).
type(butterfree,bug).
type(weedle,bug).
type(kakuna,bug).
type(beedrill,bug).
type(paras,bug).
type(parasect,bug).
type(venonat,bug).
type(venomoth,bug).
type(scyther,bug).
type(pinsir,bug).


%grass
type(bulbasaur,grass).
type(ivysaur,grass).
type(venusaur,grass).
type(oddish,grass).
type(gloom,grass).
type(vileplume,grass).
type(paras,grass).
type(parasect,grass).
type(bellsprout,grass).
type(weepinbell,grass).
type(victreebel,grass).
type(exeggcute,grass).
type(exeggutor,grass).
type(tangela,grass).

%flying

type(butterfree,flying).
type(pidgey,flying).
type(pidgeotto,flying).
type(pidgeot,flying).
type(spearow,flying).
type(fearow,flying).
type(zubat,flying).
type(golbat,flying).
type(farfetch,flying).
type(doduo,flying).
type(dodrio,flying).
type(scyther,flying).
type(gyarados,flying).
type(aerodactyl,flying).
type(articuno,flying).
type(zapdos,flying).
type(moltres,flying).
type(dragonite,flying).
type(charizard,flying).

%rock

type(geodude,rock).
type(graveler,rock).
type(golem,rock).
type(onix,rock).
type(rhyhorn,rock).
type(rhydon,rock).
type(omanyte,rock).
type(omastar,rock).
type(kabuto,rock).
type(kabutops,rock).
type(aerodactyl,rock).

%fire

type(charmander,fire).
type(charmeleon,fire).
type(vulpix,fire).
type(ninetales,fire).
type(growlithe,fire).
type(arcanine,fire).
type(ponyta,fire).
type(rapidash,fire).
type(magmar,fire).
type(flareon,fire).
type(moltres,fire).
type(charizard,fire).


%water

type(squirtle,water).
type(wartortle,water).
type(blastoise,water).
type(psyduck,water).
type(golduck,water).
type(poliwag,water).
type(poliwhirl,water).
type(poliwrath,water).
type(tentacool,water).
type(tentacruel,water).
type(slowpoke,water).
type(slowbro,water).
type(seel,water).
type(dewgong,water).
type(shellder,water).
type(cloyster,water).
type(krabby,water).
type(kingler,water).
type(horsea,water).
type(seadra,water).
type(goldeen,water).
type(seaking,water).
type(staryu,water).
type(starmie,water).
type(magikarp,water).
type(gyarados,water).
type(lapras,water).
type(vaporeon,water).
type(omanyte,water).
type(omastar,water).
type(kabuto,water).
type(kabutops,water).

%normal

type(pidgey,normal).
type(pidgeotto,normal).
type(pidgeot,normal).
type(rattata,normal).
type(raticate,normal).
type(spearow,normal).
type(fearow,normal).
type(jigglypuff,normal).
type(wigglytuff,normal).
type(meowth,normal).
type(persian,normal).
type(farfetch,normal).
type(doduo,normal).
type(dodrio,normal).
type(lickitung,normal).
type(chansey,normal).
type(kangaskhan,normal).
type(tauros,normal).
type(ditto,normal).
type(eevee,normal).
type(porygon,normal).
type(snorlax,normal).


%poison

type(bulbasaur,poison).
type(ivysaur,poison).
type(venusaur,poison).
type(weedle,poison).
type(kakuna,poison).
type(beedrill,poison).
type(ekans,poison).
type(arbok,poison).
type(nidoranF,poison).
type(nidorina,poison).
type(nidoqueen,poison).
type(nidoranM,poison).
type(nidorino,poison).
type(nidoking,poison).
type(zubat,poison).
type(golbat,poison).
type(oddish,poison).
type(gloom,poison).
type(vileplume,poison).
type(venonat,poison).
type(venomoth,poison).
type(bellsprout,poison).
type(weepinbell,poison).
type(victreebel,poison).
type(tentacool,poison).
type(tentacruel,poison).
type(grimer,poison).
type(muk,poison).
type(gastly,poison).
type(haunter,poison).
type(gengar,poison).
type(koffing,poison).
type(weezing,poison).


% ----------------------------------
% End of Pokemons
% ----------------------------------


border(X,Y) :- X == 0 ; X == 41 ; Y == 0 ; Y == 41.



%-----------------------------------
% Perceptions
%-----------------------------------

updPokeCenter(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , perfumeJoy(I,Y) , perfumeJoy(X,Iy) , perfumeJoy(D,Y) , perfumeJoy(X,Dy)), putPokeCenter(X,Y) . 

updMart(X,Y) :- (inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , screamSeller(I,Y) , screamSeller(X,Iy) , screamSeller(D,Y) , screamSeller(X,Dy)), putMart(X,Y) .

updTrainer(X,Y) :-  
(X==0 , inc(Y,Iy) , dec(Y,Dy) , screamTrainer(1,Y) , screamTrainer(X,Iy)  , screamTrainer(X,Dy) , putTrainer(X,Y) ) ;
(X==41 , inc(Y,Iy) , dec(Y,Dy) , screamTrainer(40,Y) , screamTrainer(X,Iy)  , screamTrainer(X,Dy) , putTrainer(X,Y) ) ;
(Y==0 , inc(X,I) , dec(X,D) , screamTrainer(X,1) , screamTrainer(I,Y)  , screamTrainer(D,Y) , putTrainer(X,Y) ) ;
(Y==41 , inc(X,I) , dec(X,D) , screamTrainer(X,40) , screamTrainer(I,Y)  , screamTrainer(D,Y) , putTrainer(X,Y) ) ;
(X==0 , Y=0 , inc(X,I) , inc(Y,Iy) , screamTrainer(X,Iy) , screamTrainer(I,Y) , putTrainer(X,Y) ) ;
(X==0 , Y=41 , inc(X,I) , dec(Y,D) , screamTrainer(X,D) , screamTrainer(I,Y) , putTrainer(X,Y) ) ; 
(X==41 , Y=0 , dec(X,D) , inc(Y,Iy) , screamTrainer(X,Iy) , screamTrainer(D,Y) , putTrainer(X,Y) ) ; 
(X==41 , Y=41 , dec(X,D) , dec(Y,Dy) , screamTrainer(X,Dy) , screamTrainer(D,Y) , putTrainer(X,Y) ) ;  
(inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , screamTrainer(I,Y) , screamTrainer(X,Iy)  , screamTrainer(D,Y) , screamTrainer(X,Dy) , putTrainer(X,Y)) .

tryPokeCenter(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updPokeCenter(I,Y);true) , (updPokeCenter(X,Iy);true) , (updPokeCenter(D,Y);true) , (updPokeCenter(X,Dy);true) .
tryTrainer(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updTrainer(I,Y);true) , (updTrainer(X,Iy);true) , (updTrainer(D,Y);true) , (updTrainer(X,Dy);true). 

trySeller(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , (updMart(I,Y);true) , (updMart(X,Iy);true) , (updMart(D,Y);true) , (updMart(X,Dy);true).
setSafe(X,Y) :-  inc(X,I) , inc(Y,Iy) , dec(X,D) , dec(Y,Dy) , safeLst(L) ,(

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) , ( not(visited(D,Y)) , not(safe(D,Y)) ) , ( not(visited(X,Dy)) , not(safe(X,Dy)))   , 
					includeList(I,Y,L,L1)  , includeList(X,Iy,L1,L2)   , includeList(D,Y,L2,L3) ,   includeList(X,Dy,L3,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) , ( not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) ) 	    , 
					includeList(I,Y,L,L1) ,   includeList(X,Iy,L1,L2)   , includeList(D,Y,L2,L3) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) )  , ( not(visited(X,Dy)) , not(safe(X,Dy)) )  , 
					includeList(I,Y,L,L1) ,   includeList(X,Iy,L1,L2)  , includeList(X,Dy,L2,L3) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ), ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) )  	  ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)  , includeList(X,Iy,L1,L2) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) , not(safe(D,Y)) ) , ( not(visited(X,Dy)) , not(safe(X,Dy)) )  , 
					includeList(I,Y,L,L1)   , includeList(D,Y,L1,L3) ,   includeList(X,Dy,L3,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)   , includeList(D,Y,L1,L3)  ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),( visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	    , (not(visited(X,Dy)),not(safe(X,Dy)) )   , 
					includeList(I,Y,L,L1) ,  includeList(X,Dy,L1,L4) ) ;

				(( not(visited(I,Y)) , not(safe(I,Y)) ),(visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	  ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(I,Y,L,L1)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(not(visited(D,Y)) , not(safe(D,Y)) ) , (not(visited(X,Dy)) , not(safe(X,Dy)) ),   
					includeList(X,Iy,L,L2)   , includeList(D,Y,L2,L3)   , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(not(visited(D,Y)) , not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )     , 
					includeList(X,Iy,L,L2)   , includeList(D,Y,L2,L3) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) ) 	  , (not(visited(X,Dy)) , not(safe(X,Dy)) ),  
					includeList(X,Iy,L,L3)  , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  , ( not(visited(X,Iy)) , not(safe(X,Iy)) ) ,(visited(D,Y) ; safe(D,Y) ) 	  ,(visited(X,Dy) ; safe(X,Dy) )       ,
					includeList(X,Iy,L,L3)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) ,not(safe(D,Y)) ) , (not(visited(X,Dy)) , not(safe(X,Dy))  ) , 
					includeList(D,Y,L,L3)  , includeList(X,Dy,L3,L4) ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,( not(visited(D,Y)) ,not(safe(D,Y)) ) ,(visited(X,Dy) ; safe(X,Dy) )       , 
					includeList(D,Y,L,L3)  ) ;

				((visited(I,Y) ; safe(I,Y) )	  ,(visited(X,Iy) ; safe(X,Iy) ) 	   ,(visited(D,Y) ; safe(D,Y) ) 	  , (not(visited(X,Dy)),not(safe(X,Dy))) , 
					includeList(X,Dy,L,L4) ) )  .




updPerfum(X,Y) :-  assert(perfumeJoy(X,Y)) , tryPokeCenter(X,Y) .
updPerScremS(X,Y) :- assert(screamSeller(X,Y)) , trySeller(X,Y) .
updPerScremT(X,Y,SCREAMT) :- (SCREAMT == 1 , assert(screamTrainer(X,Y)) , tryTrainer(X,Y)) ; (SCREAMT == 0 , setSafe(X,Y)) . 
updPokemon(X,Y,P) :- not(pokemon(X,Y,P)) , assert(pokemon(X,Y,P)) .
updFacing(D) :- retract(facing(X)) , assert(facing(D)) .
%-----------------------------------
% End of perceptions
%-----------------------------------





%-----------------------------------
% Facts
%-----------------------------------

at(19,24).
visited(19,24).
facing(south).
pokeball(25).
safeLst([]).
pokedex(0).

isHurt(N) :- ( hurtPokemon , N = 1) ; ( not(hurtPokemon) , N = 0 ) .

%-----------------------------------
% End Facts
%-----------------------------------


%-----------------------------------
% Best moves
%-----------------------------------


inc(A, W) :- W is A + 1.
dec(B, K) :- K is B - 1.




bestMove(healPokemon(X,Y)) :- at(X,Y) , pokeCenter(X,Y) ,  hurtPokemon , retract(hurtPokemon) .
bestMove(battleTrainer(X,Y,R)) :- at(X,Y), trainer(X,Y) , ( ( hurtPokemon , R = 0 ) ; ( not(hurtPokemon) , R = 1 , assert(hurtPokemon) , retract(trainer(X,Y)) ) ) .


% gary killer mode !
bestMove(killGary(Xg,Yg)) :- ( (pokedex(N) , N == 149) ; (safeLst(L) , isEmpty(L)) ), not(hurtPokemon) , nrstTrainer(X,Y) , Xg = X , Yg = Y , retract(at(C,D)) , assert(at(Xg,Yg)) .
bestMove(goPokeCenter(Xg,Yg)) :- ( (pokedex(N) , N == 149) ; (safeLst(L) , isEmpty(L)) ), hurtPokemon , nrstPokeCenter(X,Y) , Xg = X , Yg = Y , retract(at(C,D)) , assert(at(Xg,Yg))  .
% Final - gary killer mode !

bestMove(launchPokeball(P)) :- at(X,Y) , pokemon(X,Y,P), pokeball(N) , (  N > 0  , retract(pokemon(X,Y,P)) , dec(N,ND) , retract(pokeball(N)) , assert(pokeball(ND)) , setType(P) , pokedex(PN) , inc(PN,IPN) , retract(pokedex(PN)) , assert(pokedex(IPN)) ) .
bestMove(catchPokemon(Xg,Yg)) :- nrstPokemon(X,Y), pokemon(X,Y,P) , visited(X,Y) ,Xg = X , Yg = Y  ,pokeball(N) , N >  0 , retract(at(H,J)) , assert(at(X,Y)) .
bestMove(buyPokeball(X,Y)) :- at(X,Y) , mart(X,Y) , pokeball(N) , pokedex(D) , ND is  N + D , ND < 150 , retract(mart(X,Y)) , NM is N + 25 , retract(pokeball(N)) , assert(pokeball(NM)) .


bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D) , safe( D ,Y) , not(trainer( D ,Y))  , not(visited(D,Y)) ,  allowed(D,Y) )
											, assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y))  ,removeSafe(D,Y) .

bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I) , safe(I ,Y) , not(trainer(I ,Y)) , not(visited(I,Y)) ,allowed(I,Y) ) 
											, assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) ,removeSafe(I,Y) .

bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41 , facing(east) , inc(Y,I) , safe(X,I) , not(trainer(X,I)) , not(visited(X,I)) ,  allowed(X,I) ) 
											, assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) ,removeSafe(X,I)  .

bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , safe(X,D), not(trainer(X,D))  , not(visited(X,D)) , allowed(X,D) ) 
											, assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) ,removeSafe(X,D) .



bestMove(turnRight) :- 	(facing(north) , at(X,Y) , dec(X,D) , inc(Y,I) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,I) , allowed(X,I)  , not(visited(X,I)) ,  assert(facing(east)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , dec(Y,D) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,D) , allowed(X,D)  , not(visited(X,D)) ,  assert(facing(west)) , retract(facing(south)) );
						(facing(east) , at(X,Y) , inc(Y,I) , inc(X,IX) , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(IX,Y) ,allowed(IX,Y) , not(visited(IX,Y)) ,  assert(facing(south)) , retract(facing(east)) );
						(facing(west) , at(X,Y) , dec(Y,D) , dec(X,DX) , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(DX,Y) ,allowed(DX,Y) , not(visited(DX,Y)) ,  assert(facing(north)) , retract(facing(west)) ).

bestMove(turnLeft) :- 	(facing(north) , at(X,Y) , dec(X,D) , dec(Y,DY) , (not(safe(D,Y)) ; not(allowed(D,Y)) ; visited(D,Y) )  , safe(X,DY) ,allowed(X,DY), not(visited(X,DY)) ,  assert(facing(west)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , inc(Y,IY) , (not(safe(I,Y)) ; not(allowed(I,Y)) ; visited(I,Y) )  , safe(X,IY) ,allowed(X,IY), not(visited(X,IY)) ,  assert(facing(east)) , retract(facing(south)) );
						(facing(east) ,  at(X,Y) , inc(Y,I) , dec(X,D)  , (not(safe(X,I)) ; not(allowed(X,I)) ; visited(X,I) )  , safe(D,Y)  ,allowed(D,Y) , not(visited(D,Y)) ,  assert(facing(north)) , retract(facing(east)) );
						(facing(west) ,  at(X,Y) , dec(Y,D) , inc(X,I)  , (not(safe(X,D)) ; not(allowed(X,D)) ; visited(X,D) )  , safe(I,Y)  ,allowed(I,Y) , not(visited(I,Y)) ,  assert(facing(south)) , retract(facing(west)) ).


bestMove(moveUp(D,Y)) :- (at(X,Y) , X > 0 , facing(north) , dec(X,D)  , not(visited(D,Y)) ,  allowed(D,Y)  , not(hurtPokemon) ) , assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y)) .
bestMove(moveDown(I,Y)) :- (at(X,Y) , X < 41 , facing(south) , inc(X,I)  , not(visited(I,Y)) ,allowed(I,Y) , not(hurtPokemon) ),  assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) .
bestMove(moveRight(X,I)) :- (at(X,Y) , Y < 41  , facing(east) , inc(Y,I) , not(visited(X,I)) ,  allowed(X,I) , not(hurtPokemon) ) , assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) .
bestMove(moveLeft(X,D)) :- (at(X,Y) , Y > 0 ,  facing(west) , dec(Y,D) , not(visited(X,D)) , allowed(X,D) , not(hurtPokemon) ) , assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) .


bestMove(aStar(Xg,Yg)) :- safeLst(L) , not(isEmpty(L)) , isAllowed(H,L) , H = safe(Xg,Yg) , ( takeList(Xg,Yg,L,LR) ) , retract(at(X,Y)) , assert(at(Xg,Yg)) , assert(visited(Xg,Yg)) .


bestMove(joker(0,0)) .

%-----------------------------------
% End of Best moves
%-----------------------------------



