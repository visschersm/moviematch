# MovieMatch

Een mobile-vriendelijke web applicatie waar je films kunt beoordelen en matchen met vrienden!

## 🎬 Functionaliteit

- **Film Beoordeling**: Blader door films en geef aan of je ze leuk vindt of niet
- **IMDb-achtige Interface**: Bekijk filmdetails zoals titel, jaar, genre, rating en beschrijving
- **Connecties**: Verbind met vrienden om hun filmvoorkeuren te zien
- **Matching**: Ontdek automatisch welke films jullie beide leuk vinden

## 🚀 Aan de slag

### Vereisten

- .NET 9.0 SDK

### Installatie

1. Clone de repository:
```bash
git clone https://github.com/visschersm/moviematch.git
cd moviematch
```

2. Navigeer naar de MovieMatch folder:
```bash
cd MovieMatch
```

3. Voer de applicatie uit:
```bash
dotnet run
```

4. Open je browser en ga naar `https://localhost:5001` of `http://localhost:5000`

## 📱 Mobile-Friendly

De applicatie is volledig responsive en werkt uitstekend op mobiele apparaten.

## 🏗️ Technische Stack

- **Frontend**: Blazor Server met interactieve componenten
- **Backend**: ASP.NET Core 9.0
- **Database**: SQLite met Entity Framework Core
- **UI**: Bootstrap 5 + Custom CSS

## 📂 Project Structuur

```
MovieMatch/
├── Components/
│   ├── Layout/          # Layout componenten (NavMenu, MainLayout)
│   └── Pages/           # Pagina's (Home, Movies, Connections, Matches)
├── Data/                # Database context
├── Models/              # Data modellen (Movie, User, UserRating, UserConnection)
├── Services/            # Business logic services
└── wwwroot/             # Statische bestanden
```

## 🎯 Gebruik

1. **Film Beoordelen**: Ga naar de Movies pagina en swipe door films door op Like of Dislike te klikken
2. **Connecten**: Ga naar Connections en verbind met vrienden (Jane Smith of Bob Johnson)
3. **Matches Bekijken**: Ga naar Matches om films te zien die jij en je vrienden beide leuk vinden

## 🌟 Features

- Responsive design voor mobiel en desktop
- Real-time updates met Blazor Server
- SQLite database met seed data
- Mooie filmposters via IMDb URLs
- Intuïtieve swipe-achtige interface voor film beoordeling