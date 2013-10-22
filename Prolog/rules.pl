
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


at(24,19).

pokemon(10,25,pikachu).

perfumeJoy(X,Y) :- pokeCenter(X+1,Y) ; pokeCenter(X,Y+1) ; pokeCenter(X-1,Y) ; pokeCenter(X,Y-1).
assert(pokeCenter(X,Y)) :- perfumeJoy(X+1,Y) , perfumeJoy(X,Y+1) , perfumeJoy(X-1,Y) , perfumeJoy(X,Y-1).

shoutSeller(X,Y) :- mart(X+1,Y) ; mart(X,Y+1) ; mart(X-1,Y) ; mart(X,Y-1).
assert(mart(X,Y)) :- shoutSeller(X+1,Y) , shoutSeller(X,Y+1) , shoutSeller(X-1,Y) , shoutSeller(X,Y-1).

shoutTrainer(X,Y) :- trainer(X+1,Y) ; trainer(X,Y+1) ; trainer(X-1,Y) ; trainer(X,Y-1).
assert(trainer(X,Y)) :- shoutTrainer(X+1,Y) , shoutTrainer(X,Y+1)  , shoutTrainer(X-1,Y) , shoutTrainer(X,Y-1).

assert(captured(P)) :- launchPokeball(P).

assert(visited(X,Y)) :- at(X,Y).

safe(X, Y) :- not trainer(X,Y).


bestMove(launchPokeball(P)) :- at(X,Y) , pokemon(X,Y,P).
bestMove(healPokemon(X,Y)) :- at(X,Y) , pokeCenter(X,Y).
bestMove(buyPokeball(X,Y)) :- at(X,Y) , mart(X,Y).

addList(X,L,[X|L]).
