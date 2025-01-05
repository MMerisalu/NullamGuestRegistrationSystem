# Käesolev README sisaldab juhiseid rakenduse Nullam käivitamiseks.

# Rakenduse kirjeldus
 Nullam külaliste registreerimissüsteem, mis võimaldab lisada/muuta tulevikus toimuvaid üritusi ja vaadata juba toimunud üritusi. 
 
# Rakenduse serveri poole käivitamine

Rakenduse serveri poole käivitamiseks, avage kaustas NullamGuestRegistrationSystemSolution NullamGuestRegistrationSystemSolution.sln fail.

# Andmebaasi uuendamine

Käesolev rakendus sisaldab sqlite andmebaasi faili, mille saab opereerida järgnevate käsude abil.
Andmebaasi kustutamine : Drop-Database -p App.DAL.EF -s WebApp
Andmebaasi uuendamine: Update-Database -p App.DAL.EF -s WebApp


# Nullam klient rakenduse käivitamine

Nullam klient rakenduse käivitamiseks, avage kaust nullam-guest-registration-system-client. 
Vajaminevate npm paketide paigaldamiseks, sisestage terminali aknasse käsk npm i
Rakenduse kliendipoolse rakenduse käivitamiseks, sisestage terminali aknasse käsk npm run dev