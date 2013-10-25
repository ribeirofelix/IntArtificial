% ----------------------------------
% Pokemons
% ----------------------------------

% fairy
fairy(clefairy).
fairy(clefable).
fairy(jigglypuff).
fairy(wigglytuff).
fairy(mr_Mime).

% steel
steel(magnemite).
steel(magneton).

% ghost

ghost(gastly).
ghost(haunter).
ghost(gengar).

% ice 
ice(dewgong).
ice(cloyster).
ice(jynx).
ice(lapras).
ice(articuno).

% pground

pground(sandshrew).
pground(sandslash).
pground(nidoqueen).
pground(nidoking).
pground(diglett).
pground(dugtrio).
pground(geodude).
pground(graveler).
pground(golem).
pground(onix).
pground(cubone).
pground(marowak).
pground(rhyhorn).
pground(rhydon).


% fighting

fighting(mankey).
fighting(primeape).
fighting(poliwrath).
fighting(machop).
fighting(machoke).
fighting(machamp).
fighting(hitmonlee).
fighting(hitmonchan).

% dragon
dragon(dratini).
dragon(dragonair).
dragon(dragonite).


% psychic

psychic(abra).
psychic(kadabra).
psychic(alakazam).
psychic(slowpoke).
psychic(slowbro).
psychic(drowzee).
psychic(hypno).
psychic(exeggcute).
psychic(exeggutor).
psychic(starmie).
psychic(mr_Mime).
psychic(jynx).
psychic(mewtwo).

% eletric

electric(pikachu).
electric(raichu).
electric(magnemite).
electric(magneton).
electric(voltorb).
electric(electrode).
electric(electabuzz).
electric(jolteon).
electric(zapdos).


% bug

bug(caterpie).
bug(metapod).
bug(butterfree).
bug(weedle).
bug(kakuna).
bug(beedrill).
bug(paras).
bug(parasect).
bug(venonat).
bug(venomoth).
bug(scyther).
bug(pinsir).


% grass
grass(bulbasaur).
grass(ivysaur).
grass(venusaur).
grass(oddish).
grass(gloom).
grass(vileplume).
grass(paras).
grass(parasect).
grass(bellsprout).
grass(weepinbell).
grass(victreebel).
grass(exeggcute).
grass(exeggutor).
grass(tangela).

% flying

flying(butterfree).
flying(pidgey).
flying(pidgeotto).
flying(pidgeot).
flying(spearow).
flying(fearow).
flying(zubat).
flying(golbat).
flying(farfetch).
flying(doduo).
flying(dodrio).
flying(scyther).
flying(gyarados).
flying(aerodactyl).
flying(articuno).
flying(zapdos).
flying(moltres).
flying(dragonite).
flying(charizard).

% rock

rock(geodude).
rock(graveler).
rock(golem).
rock(onix).
rock(rhyhorn).
rock(rhydon).
rock(omanyte).
rock(omastar).
rock(kabuto).
rock(kabutops).
rock(aerodactyl).

% fire

fire(charmander).
fire(charmeleon).
fire(vulpix).
fire(ninetales).
fire(growlithe).
fire(arcanine).
fire(ponyta).
fire(rapidash).
fire(magmar).
fire(flareon).
fire(moltres).
fire(charizard).


% water

water(squirtle).
water(wartortle).
water(blastoise).
water(psyduck).
water(golduck).
water(poliwag).
water(poliwhirl).
water(poliwrath).
water(tentacool).
water(tentacruel).
water(slowpoke).
water(slowbro).
water(seel).
water(dewgong).
water(shellder).
water(cloyster).
water(krabby).
water(kingler).
water(horsea).
water(seadra).
water(goldeen).
water(seaking).
water(staryu).
water(starmie).
water(magikarp).
water(gyarados).
water(lapras).
water(vaporeon).
water(omanyte).
water(omastar).
water(kabuto).
water(kabutops).

% normal

normal(pidgey).
normal(pidgeotto).
normal(pidgeot).
normal(rattata).
normal(raticate).
normal(spearow).
normal(fearow).
normal(jigglypuff).
normal(wigglytuff).
normal(meowth).
normal(persian).
normal(farfetch).
normal(doduo).
normal(dodrio).
normal(lickitung).
normal(chansey).
normal(kangaskhan).
normal(tauros).
normal(ditto).
normal(eevee).
normal(porygon).
normal(snorlax).


% poison

poison(bulbasaur).
poison(ivysaur).
poison(venusaur).
poison(weedle).
poison(kakuna).
poison(beedrill).
poison(ekans).
poison(arbok).
poison(nidoranF).
poison(nidorina).
poison(nidoqueen).
poison(nidoranM).
poison(nidorino).
poison(nidoking).
poison(zubat).
poison(golbat).
poison(oddish).
poison(gloom).
poison(vileplume).
poison(venonat).
poison(venomoth).
poison(bellsprout).
poison(weepinbell).
poison(victreebel).
poison(tentacool).
poison(tentacruel).
poison(grimer).
poison(muk).
poison(gastly).
poison(haunter).
poison(gengar).
poison(koffing).
poison(weezing).


% ----------------------------------
% End of Pokemons
% ----------------------------------

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
:- dynamic safe/2.
:- dynamic facing/1.
:- dynamic at/2.
:- dynamic visited/2.

%-----------------------------------
% End of dynamic procedures
%-----------------------------------



%-----------------------------------
% Perceptions
%-----------------------------------

pokeCenter(X,Y) :- (perfumeJoy(X+1,Y) , perfumeJoy(X,Y+1) , perfumeJoy(X-1,Y) , perfumeJoy(X,Y-1)), assert(pokeCenter(X,Y)). 

mart(X,Y) :- (screamSeller(X+1,Y) , screamSeller(X,Y+1) , screamSeller(X-1,Y) , screamSeller(X,Y-1)), assert(mart(X,Y)).

trainer(X,Y) :- (screamTrainer(X+1,Y) , screamTrainer(X,Y+1)  , screamTrainer(X-1,Y) , screamTrainer(X,Y-1)) , assert(trainer(X,Y)).

% E SE TREINADOR ESTIVER EM X,Y+1, ASH EM X,Y E TREINADOR EM X+1,Y? QUANTAS PERCEPCOES?
upPerc(X,Y,PERFUME,SCREAMS,SCREAMT):- ((PERFUME == 1 , assert(perfumeJoy(X,Y))) , pokeCenter(X+1,Y) , pokeCenter(X,Y+1) , pokeCenter(X-1,Y) , pokeCenter(X,Y-1)) ;
									  ((SCREAMT == 1 , assert(screamTrainer(X,Y))) , trainer(X+1,Y) , trainer(X,Y+1) , trainer(X-1,Y) , trainer(X,Y-1)) ;
									  ((SCREAMS == 1 , assert(screamSeller(X,Y))) , mart(X+1,Y) , mart(X,Y+1) , mart(X-1,Y) , mart(X,Y-1)) ;
									  (SCREAMT == 0 , assert(safe(X+1,Y)) , assert(safe(X,Y+1)) , assert(safe(X-1,Y)) , assert(safe(X,Y-1))).

%-----------------------------------
% End of perceptions
%-----------------------------------


%-----------------------------------
% Some rules
%-----------------------------------



% se a pokebola foi lancada, o pokemon foi capturado 
% captured(P) :- launchPokeball(P).

% se ha um treinador, tem batalha
battle(X,Y) :- trainer(X,Y), at(X,Y).

% se há batalha e os pokemons estão curados, há vitória
victory(X,Y) :- battle(X,Y) , not(hurtPokemon). 

% se há batalha e os pokemons não estão curados, há derrota
defeat(X,Y) :- battle(X,Y) , hurtPokemon.

% se há batalha, os pokemons estão machucados
hurtPokemon :- battle(_,_). 

% se ash está em X,Y, então este local foi visitado
visited(X,Y) :- at(X,Y), assert(visited(X,Y)).

% se ash é vitorioso, o treinador é retirado
trainerDefeated(X,Y) :- (at(X,Y) , victory(X,Y)) , retract(trainer(X,Y)) , assert(safe(X,Y)).

%-----------------------------------
% End of some rules
%-----------------------------------



%-----------------------------------
% Facts
%-----------------------------------

at(24,19).
safe(25,19).
safe(23,19).
safe(24,18).
safe(24,20).
safe(24,19).
pokemon(25,19,pikachu).
facing(south) .

%-----------------------------------
% End Facts
%-----------------------------------


%-----------------------------------
% Best moves
%-----------------------------------


% verifica vizinhas e alguem nao  visitado , vai pra la
% nenhum visitado , entao pega o primeiro safe
% todos visitados entao pega o primeiro safe da lista( ou da regra )

inc(A, W) :- W is A + 1.
dec(B, K) :- K is B - 1.
        

bestMove(moveUp(D,Y)) :- (at(X,Y) , X \== 0 , facing(north) , dec(X,D) , safe( D ,Y) , not(visited(D,Y))) , assert(at(D,Y)) , retract(at(X,Y)) , assert(visited(D,Y)) .

bestMove(moveDown(I,Y)) :- (at(X,Y) , X \== 41 , facing(south) , inc(X,I) , safe(I ,Y) , not(visited(I,Y))) , assert(at(I,Y)) , retract(at(X,Y)) , assert(visited(I,Y)) .

bestMove(moveRight(X,I)) :- (at(X,Y) , Y \== 0 , facing(east) , inc(Y,I) , safe(X,I) , not(visited(X,I))) , assert(at(X,I)) , retract(at(X,Y)) , assert(visited(X,I)) .

bestMove(moveLeft(X,D)) :- (at(X,Y) , Y \== 41 ,  facing(west) , dec(Y,D) , safe(X,D) , not(visited(X,D))) , assert(at(X,D)) , retract(at(X,Y)) , assert(visited(X,D)) .

% mais uma regra pra aleatorio.

% olhar nao visitados

bestMove(launchPokeball(P)) :- (at(X,Y) , pokemon(X,Y,P)) , retract(pokemon(X,Y,P)) , assert(safe(X,Y)) .

bestMove(healPokemon(X,Y)) :- at(X,Y) , pokeCenter(X,Y).

bestMove(buyPokeball(X,Y)) :- at(X,Y) , mart(X,Y).

bestMove(battleTrainer(X,Y)) :- battle(X,Y).

bestMove(turnRight) :- 	(facing(north) , at(X,Y) , dec(X,D) , not(safe(D,Y)) ,  assert(facing(east)) , retract(facing(north)) );
						(facing(south) , at(X,Y) , inc(X,I) , not(safe(I,Y)) ,  assert(facing(west)) , retract(facing(south)) );
						(facing(east) , at(X,Y) , inc(Y,I) , not(safe(X,I)) ,  assert(facing(south)) , retract(facing(east)) );
						(facing(west) , at(X,Y) , dec(Y,D) , not(safe(X,D)) ,  assert(facing(north)) , retract(facing(west)) ).

bestMove(turnLeft) :- 	(facing(north) , dec(X,D) , not(safe(D,Y)) ,  assert(facing(west)) , retract(facing(north)) );
						(facing(south) , inc(X,I) , not(safe(I,Y)) ,  assert(facing(east)) , retract(facing(south)) );
						(facing(east) , inc(Y,I) , not(safe(X,I)) ,  assert(facing(north)) , retract(facing(east)) );
						(facing(west) ,  dec(Y,D) , not(safe(X,D)) ,  assert(facing(south)) , retract(facing(west)) ).

%-----------------------------------
% End of Best moves
%-----------------------------------
