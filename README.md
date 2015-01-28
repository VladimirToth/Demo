This is our simple application for windows 8.1 

Ako by mala aplikacia fungovat: po zapnuti sa objavi splashscreen, potom page s loginom, po neuspesnom logine to upozorni, ze nieco uzivatel zadal zle. 
Ak bude login uspesny, tak pojde na strnaku, kde si vyberie spoj - departure station a destination station - po potvrdeni vyberu sa zobrazia informacie o pocte kilometrov medzi zadanymi stanicami, najblizsie odchody podla aktualneho casu v jeho pocitaci, cas trvania cesty a cena listka spolu s tlacitkom, ktore po potvrdeni presmeruje uzivatela na stranku kde moze kupovat listok a bookovat pocet miest.

Team members are: Vlado, Majo, Erik, and Peter

We have to assign work to each other.

peter - json file or c# code which creates json, testing, debugging

majo - splashscreen vratane uvodnej grafiky pripadne celkovej grafiky aplikacie, porovnanie casov aby ukazalo najblizsi cas odchodu vlaku,

vlado - hlavna funkcionalita, vyber nastupnej zastavky, vyber destinacie, vypocet ceny listka, zobrazenie casu cesty, dlzky cesty v kilometroch a button, ktory presmeruje uzivatela na dalsiu stranku.

erik - login page, ktora pri prvom spusteni vyzve na zaregitrovanie sa do aplikacie - udaje o registracii ulozi do json file a nasledne vyzve uzivatela na prihlasenie cez prihlasovacie meno a heslo. V pripade nespravneho zadania udajov vypise hlasku, ze prihlasovacie meno alebo heslo boli nespravne. V pripade uspesneho zadania mena a hesla prejde uzivatel na hlavnu stranku aplikacie, ktoru ma starosti vlado.

Aktualne nezaradene ulohy> stranka, na ktoru uzivatela presmeruje button z hlavnej stranky ked si bude chciet uzivatel kupit listok. Na tejto stranke si moze uzivatel kupit listok a zabookovat pocet miest.

moznost vytiahnut login z OS
