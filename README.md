# Bug Hunt - Debugging Oefeningen

Deze Visual Studio Solution (`BugHunt.slnx`) bevat drie consoleprojecten vol syntaxisfouten en verborgen logische bugs:

1. **`01-BugHuntWarmup`**: 6 korte mini-puzzels (één per module) om op te warmen.
2. **`02-Recyclagepark`**: registreer bezoekers en afvalfracties op het containerpark en druk een correcte wegingsbon af.
3. **`03-MonsterArena`**: een turn-based gevecht tussen een held en een draak.

## Werkwijze in Visual Studio

1. Klik in het **Solution Explorer**-venster met de rechtermuisknop op het project waaraan je wil werken en kies **Set as Startup Project**.
2. Los eerst de **syntaxisfouten** (rode golflijntjes) op zodat het project kan compileren.
3. Plaats **breakpoints (`F9`)** en start het programma met **`F5`**.
4. Gebruik **Step Over (`F10`)**, **Step Into (`F11`)** en de vensters **Locals / Watch** om de logische fouten op te sporen en te herstellen.

