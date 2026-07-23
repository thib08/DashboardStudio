# Dashboard Studio — Architecture Technique v1.0

## 1. Objet

Ce document définit l'architecture technique de référence de Dashboard Studio.

Le projet est composé de deux applications :

- **Dashboard Studio** : application Windows de conception et d'administration.
- **Dashboard Runtime** : application légère exécutée sur Raspberry Pi et affichant les dashboards sur l'écran tactile.

Objectifs : architecture moderne, claire, maintenable, testable, performante et facile à faire évoluer.

---

## 2. Stack technique

| Domaine | Choix |
|---|---|
| Langage | C# |
| Framework | .NET |
| Studio | WPF |
| Runtime | Avalonia UI |
| Architecture UI | MVVM |
| Tests | xUnit |
| Découverte réseau | mDNS / DNS-SD |
| Données | Data Providers |
| Commandes | Actions |
| Automatisation | Events + Conditions + Actions |
| Synchronisation | Différentielle |
| Déploiement | Staging + activation atomique |
| Cache | Local persistant |
| Distribution Studio | Portable + Installateur |
| Runtime | Raspberry Pi |
| Interface Runtime | Tactile |
| Thèmes Studio | Clair + Sombre |

La version exacte de .NET sera celle d'une version LTS appropriée au moment de la création effective de la solution.

---

## 3. Principes architecturaux

### Séparation des responsabilités

- L'interface ne connaît pas les détails du réseau.
- Le Runtime ne connaît pas les détails de l'éditeur.
- Les widgets ne communiquent pas directement avec MSFS.
- Les Data Providers fournissent les données.
- Les Actions exécutent les commandes.
- Les événements déclenchent les automatisations.

### Code partagé

Les modèles et contrats communs sont regroupés dans des bibliothèques .NET partagées entre Studio et Runtime lorsque cela est pertinent.

### Architecture pragmatique

Le projet n'utilise pas de microservices. Il repose sur plusieurs bibliothèques .NET et deux applications principales.

### Fonctionnement hors ligne

Le Runtime conserve localement la dernière configuration valide et continue à afficher le dashboard en cas de perte du réseau.

---

## 4. Structure de la solution Visual Studio

```text
DashboardStudio/
├── DashboardStudio.sln
├── src/
│   ├── DashboardStudio.App/
│   ├── DashboardStudio.Runtime/
│   ├── DashboardStudio.Core/
│   ├── DashboardStudio.Models/
│   ├── DashboardStudio.Contracts/
│   ├── DashboardStudio.Infrastructure/
│   ├── DashboardStudio.Network/
│   ├── DashboardStudio.Data/
│   ├── DashboardStudio.Actions/
│   ├── DashboardStudio.Plugins/
│   ├── DashboardStudio.Windows/
│   └── DashboardStudio.MSFS/
├── tests/
│   ├── DashboardStudio.Core.Tests/
│   ├── DashboardStudio.Network.Tests/
│   ├── DashboardStudio.Data.Tests/
│   └── DashboardStudio.MSFS.Tests/
├── docs/
├── assets/
└── scripts/
```

---

## 5. Responsabilités des projets

### DashboardStudio.App

Application WPF principale.

Contient :

- fenêtres ;
- vues ;
- ViewModels ;
- navigation ;
- éditeur ;
- design system.

Ne contient pas directement la logique MSFS ou le protocole réseau bas niveau.

### DashboardStudio.Runtime

Application Avalonia exécutée sur Raspberry Pi.

Contient :

- rendu ;
- navigation tactile ;
- gestes ;
- affichage ;
- cache ;
- synchronisation ;
- état Runtime.

### DashboardStudio.Core

Logique métier indépendante de WPF et Avalonia :

- dashboards ;
- pages ;
- widgets ;
- composants ;
- templates ;
- grille ;
- responsive layout ;
- validation ;
- moteur d'événements.

### DashboardStudio.Models

Modèles :

- Project ;
- Dashboard ;
- Page ;
- Widget ;
- Component ;
- Template ;
- Terminal ;
- Asset ;
- DataBinding ;
- ActionDefinition ;
- EventDefinition.

### DashboardStudio.Contracts

Contrats de communication :

- HelloMessage ;
- RuntimeInfo ;
- DashboardManifest ;
- SyncRequest ;
- SyncResponse ;
- AssetRequest ;
- AssetResponse ;
- CommandMessage ;
- EventMessage.

### DashboardStudio.Infrastructure

Services génériques :

- fichiers ;
- JSON ;
- cache ;
- hashing ;
- sérialisation ;
- logging ;
- configuration.

### DashboardStudio.Network

- découverte ;
- communication ;
- synchronisation ;
- transfert ;
- authentification réseau.

### DashboardStudio.Data

Abstractions et implémentations des Data Providers.

### DashboardStudio.Actions

Moteur d'actions et fournisseurs d'actions.

### DashboardStudio.Plugins

Chargement, validation et cycle de vie des plugins.

### DashboardStudio.Windows

Intégrations Windows.

### DashboardStudio.MSFS

Intégration Microsoft Flight Simulator.

---

## 6. Dépendances

```text
App
├── Core
├── Models
├── Contracts
├── Infrastructure
├── Network
├── Data
├── Actions
├── Plugins
├── Windows
└── MSFS

Runtime
├── Core
├── Models
├── Contracts
├── Infrastructure
├── Network
├── Data
└── Actions

Core
├── Models
└── Contracts

Network
├── Models
├── Contracts
└── Infrastructure

Data
├── Models
├── Contracts
└── Core

Actions
├── Models
├── Contracts
└── Core
```

Les dépendances circulaires sont interdites.

---

## 7. Architecture MVVM

Dashboard Studio utilise MVVM :

```text
View
  ↓
ViewModel
  ↓
Application Services
  ↓
Core / Infrastructure
```

La View ne contient pas de logique métier.

Le ViewModel orchestre l'interface.

Le Core contient les règles métier.

---

## 8. Design de Dashboard Studio

L'interface est moderne, claire et organisée autour de trois zones principales.

```text
┌──────────────────────────────────────────────┐
│ Toolbar                                      │
├────────────┬──────────────────────┬──────────┤
│ Navigation │ Canvas               │ Inspector│
│            │                      │          │
│ Pages      │      Dashboard      │ Properties│
│ Components │                      │          │
│ Templates  │                      │          │
├────────────┴──────────────────────┴──────────┤
│ Component Library                            │
└──────────────────────────────────────────────┘
```

### Zone gauche

- projets ;
- dashboards ;
- pages ;
- composants ;
- templates.

### Zone centrale

- canvas ;
- grille ;
- aperçu ;
- déplacement ;
- redimensionnement ;
- sélection.

### Zone droite

- propriétés ;
- position ;
- taille ;
- apparence ;
- bindings ;
- actions ;
- événements.

### Zone inférieure

- bibliothèque de composants ;
- recherche ;
- catégories ;
- glisser-déposer.

---

## 9. Design System

Deux thèmes sont prévus :

- Clair ;
- Sombre.

Les couleurs sont centralisées dans des ressources de thème.

```text
Theme
├── Background
├── Surface
├── SurfaceSecondary
├── TextPrimary
├── TextSecondary
├── Accent
├── Border
├── Success
├── Warning
└── Error
```

Les composants n'utilisent pas de couleurs codées en dur.

---

## 10. Système de grille

Un dashboard utilise une grille logique.

Un widget possède :

```text
X
Y
Width
Height
MinWidth
MaxWidth
MinHeight
MaxHeight
```

Le moteur valide chaque modification.

Le redimensionnement est contraint et adapté à la grille.

---

## 11. Responsive Dashboard

Le Runtime calcule la présentation à partir de :

- largeur ;
- hauteur ;
- ratio ;
- orientation.

La configuration logique reste indépendante de la résolution physique.

La détection automatique de l'écran est privilégiée.

---

## 12. Widgets et composants

Un widget possède notamment :

```text
Id
Type
Position
Size
Properties
Bindings
Actions
Events
Style
```

Un widget ne connaît pas directement sa source de données.

Architecture :

```text
Widget
  ↓
Data Binding
  ↓
Data Provider
  ↓
Source
```

Les composants regroupent plusieurs widgets et éléments visuels.

Les templates regroupent des structures réutilisables.

---

## 13. Data Providers

Interface principale :

```text
IDataProvider
```

Responsabilités :

- déclarer les données disponibles ;
- fournir les valeurs ;
- signaler les changements ;
- gérer les erreurs.

Exemples :

```text
WindowsSystemProvider
WindowsMediaProvider
MsfsProvider
```

Les données peuvent être :

- périodiques ;
- événementielles ;
- à la demande.

Exemple :

```text
CPU              → périodique
MSFS altitude    → temps réel
Titre musical    → événementiel
```

---

## 14. Actions

Une action possède :

```text
Id
Type
Parameters
Permissions
```

Exemples :

```text
OpenApplication
SetVolume
MediaPlay
MediaPause
MsfsCommand
ChangeDashboard
```

Architecture :

```text
Interaction
  ↓
Action
  ↓
Action Provider
  ↓
Système cible
```

---

## 15. Événements et automatisations

Architecture :

```text
Trigger
  ↓
Conditions
  ↓
Actions
```

Exemple :

```text
WHEN MsfsStarted
THEN OpenDashboard("MSFS")
```

Autre exemple :

```text
WHEN AircraftChanged
IF Aircraft == "A320"
THEN LoadProfile("A320")
```

---

## 16. Découverte des Raspberry Pi

Le Runtime annonce sa présence sur le réseau local via mDNS / DNS-SD.

Exemple de service logique :

```text
_dashboard-runtime._tcp.local
```

Dashboard Studio recherche automatiquement les Runtime disponibles.

Une configuration manuelle de secours pourra être prévue.

---

## 17. Connexion

Cycle normal :

```text
DISCOVERED
    ↓
CONNECTING
    ↓
AUTHENTICATING
    ↓
CONNECTED
    ↓
SYNCED
```

Après perte réseau :

```text
CONNECTED
    ↓
DISCONNECTED
    ↓
RECONNECTING
    ↓
CONNECTED
```

La reconnexion est automatique.

---

## 18. Authentification et appairage

Lors du premier appairage :

```text
Dashboard Studio
      ↓
Runtime détecté
      ↓
Demande d'association
      ↓
Validation utilisateur
      ↓
Terminal approuvé
```

Un terminal non approuvé ne reçoit pas de commandes sensibles.

---

## 19. Communication réseau

Deux catégories de communication sont prévues.

### Canal contrôle

Pour :

- état ;
- commandes ;
- événements ;
- synchronisation.

### Canal ressources

Pour :

- images ;
- assets ;
- fichiers plus volumineux.

Les communications sont asynchrones.

Le protocole utilise des messages structurés et versionnés.

---

## 20. Synchronisation

Le Runtime et Studio possèdent un manifeste.

```text
Dashboard Manifest
├── Version
├── Components
├── Widgets
├── Assets
└── Hashes
```

Les hashes permettent de déterminer les ressources déjà présentes.

Exemple :

```text
Studio : Asset A = HASH1
Runtime : Asset A = HASH1
→ aucun transfert

Studio : Asset B = HASH2
Runtime : Asset B = HASH3
→ transfert de Asset B uniquement
```

---

## 21. Publication et déploiement

Cycle :

```text
Draft
  ↓
Validation
  ↓
Build
  ↓
Manifest
  ↓
Diff
  ↓
Transfer
  ↓
Apply
  ↓
Activate
```

Le Runtime utilise :

```text
active/
staging/
```

La nouvelle version est préparée dans `staging`.

Après validation :

```text
staging → active
```

En cas d'échec, l'ancienne version reste active.

Cela évite les installations partielles.

---

## 22. Cache local

Structure logique :

```text
RuntimeData/
├── active/
├── staging/
├── assets/
├── cache/
├── logs/
└── runtime.json
```

Le cache est persistant.

Le Runtime peut fonctionner sans le PC lorsque la dernière configuration valide est disponible.

---

## 23. Reconnexion

Au retour du réseau :

```text
Reconnect
  ↓
Handshake
  ↓
Compare versions
  ↓
Calcul du diff
  ↓
Synchronisation
```

La synchronisation reprend automatiquement.

---

## 24. Priorité des données

Les flux sont classés :

```text
CRITICAL
HIGH
NORMAL
LOW
```

Exemple :

```text
Commandes critiques MSFS → HIGH
Données CPU             → NORMAL
Artwork                 → LOW
```

Un transfert d'asset ne doit pas bloquer les données importantes.

---

## 25. Windows

Les intégrations Windows sont isolées :

```text
DashboardStudio.Windows
├── System
├── Media
├── Process
└── Power
```

Données potentielles :

- CPU ;
- GPU ;
- RAM ;
- stockage ;
- réseau ;
- températures disponibles ;
- volume ;
- média ;
- état des applications.

---

## 26. Microsoft Flight Simulator

Architecture :

```text
DashboardStudio.MSFS
├── Connection
├── Data
├── Aircraft
├── Commands
└── Profiles
```

Le module MSFS expose des données normalisées au reste de l'application.

États suivis :

```text
MSFS arrêté
MSFS lancé
Session active
Vol chargé
Vol terminé
MSFS fermé
```

---

## 27. Détection de l'avion

Processus :

```text
Aircraft ID
  ↓
Aircraft Profile Registry
  ↓
Profil trouvé ?
├── Oui → Profil spécifique
└── Non → Profil générique
```

Un profil peut définir :

```text
AircraftProfile
├── Id
├── Name
├── Dashboard
├── DefaultPage
├── Bindings
├── Actions
└── Automation
```

---

## 28. Navigation tactile Runtime

Gestes :

```text
Swipe Left
→ Page suivante

Swipe Right
→ Page précédente

Swipe Up
→ Dashboard Launcher

Swipe Down
→ Centre de contrôle
```

Les interactions directes avec un widget ont priorité sur les gestes globaux.

Les gestes doivent être distingués de :

- Tap ;
- LongPress ;
- Drag ;
- Resize.

---

## 29. Dashboard Launcher

Le Launcher affiche en priorité :

- favoris ;
- dashboards récents.

La limite de trois concerne l'affichage principal des favoris, pas le nombre total de dashboards.

Tous les dashboards restent accessibles depuis une vue secondaire.

---

## 30. Centre de contrôle

Le Centre de contrôle peut afficher :

- état réseau ;
- connexion PC ;
- état Runtime ;
- luminosité ;
- volume ;
- dashboard actif ;
- accès aux paramètres.

---

## 31. Changement automatique

Exemple :

```text
MSFS Started
→ Dashboard MSFS

Flight Loaded
→ Cockpit

Aircraft Changed
→ Aircraft Profile
```

Le moteur d'événements pilote ces changements.

---

## 32. Plugins

Les plugins officiels peuvent fournir :

- widgets ;
- Data Providers ;
- Actions ;
- événements.

Manifest :

```text
PluginManifest
├── Id
├── Name
├── Version
├── ApiVersion
└── Capabilities
```

Les plugins incompatibles ou invalides ne sont pas chargés.

---

## 33. Stockage des projets

Structure recommandée :

```text
MyProject/
├── project.json
├── dashboards/
│   ├── desktop.json
│   └── msfs.json
├── components/
├── templates/
├── assets/
└── profiles/
```

Les formats sont versionnés et migrables.

Exemple :

```json
{
  "schemaVersion": 1
}
```

Les assets possèdent un hash pour éviter les transferts inutiles.

---

## 34. Performance Raspberry Pi

Le Runtime doit privilégier :

- faible consommation CPU ;
- mémoire maîtrisée ;
- rendu ciblé ;
- cache ;
- chargement différé ;
- ressources réutilisées ;
- absence de recalcul global inutile.

La Raspberry Pi 3 est considérée comme une machine aux ressources limitées.

Le Runtime doit démarrer automatiquement en plein écran.

---

## 35. Fonctionnement hors ligne

Au démarrage :

```text
Linux
  ↓
Dashboard Runtime
  ↓
Connexion réseau
  ↓
Chargement du cache
  ↓
Dashboard affiché
```

Si le PC est indisponible :

```text
Runtime
  ↓
Charge active/
  ↓
Affiche le dashboard
```

Les fonctions dépendant du PC sont signalées comme indisponibles.

Le dashboard lui-même reste visible.

---

## 36. Diagnostic

Le mode diagnostic peut afficher :

- CPU ;
- RAM ;
- FPS ;
- latence réseau ;
- taille du cache ;
- état de synchronisation ;
- version Runtime.

Les logs peuvent être exportés.

---

## 37. Tests

### Tests unitaires

- grille ;
- responsive layout ;
- modèles ;
- validation ;
- synchronisation.

### Tests d'intégration

- réseau ;
- Runtime ;
- déploiement ;
- MSFS.

### Tests de résilience

- perte réseau ;
- redémarrage ;
- fichier corrompu ;
- asset manquant ;
- reprise après reconnexion.

---

## 38. CI/CD

Pipeline recommandée :

```text
Push
  ↓
Build
  ↓
Tests
  ↓
Package
  ↓
Artifact
```

La publication officielle est déclenchée par une Release Git.

---

## 39. Packaging

Dashboard Studio :

- version portable ;
- installateur Windows.

Dashboard Runtime :

- package Raspberry Pi ;
- script d'installation ;
- configuration du démarrage automatique.

---

## 40. Ordre de développement

```text
1. Créer la solution
2. Créer les bibliothèques
3. Créer Models
4. Créer Contracts
5. Créer Core
6. Ajouter les tests
7. Créer Studio WPF
8. Créer Runtime Avalonia
9. Implémenter Network
10. Implémenter Discovery
11. Implémenter Sync
12. Implémenter Cache
13. Créer Data Providers
14. Créer Actions
15. Ajouter Windows
16. Ajouter MSFS
17. Créer Components
18. Créer Templates
19. Ajouter Automations
20. Optimiser
21. Packager
```

---

## 41. Première version fonctionnelle

La première boucle fonctionnelle doit être :

```text
PC
│
│ Dashboard Studio
│
├── Créer dashboard
├── Ajouter widget
├── Sauvegarder
│
▼
Wi-Fi
│
▼
Raspberry Pi
│
│ Dashboard Runtime
│
└── Afficher dashboard
```

Puis :

```text
Modifier dashboard
  ↓
Publier
  ↓
Synchroniser
  ↓
Runtime mis à jour
```

Cette boucle doit être stable avant l'ajout des fonctions MSFS avancées.

---

## 42. Règles de développement

1. Une responsabilité claire par projet.
2. Pas de dépendances circulaires.
3. Pas de logique métier dans les Views.
4. Les widgets utilisent les Data Providers.
5. Les commandes utilisent les Actions.
6. Les événements utilisent le moteur d'automatisation.
7. Les intégrations externes sont isolées.
8. Les formats de données sont versionnés.
9. Les déploiements sont atomiques.
10. Le Runtime doit toujours conserver une configuration valide.
11. Les fonctionnalités critiques sont testées.
12. Toute fonctionnalité importante est documentée.
13. La simplicité est privilégiée lorsque deux solutions sont équivalentes.
14. Les performances de la Raspberry Pi sont prises en compte dès la conception.

---

## 43. Conclusion

L'architecture retenue sépare clairement :

```text
Conception
Dashboard Studio
        ↓
Données
Data Providers
        ↓
Commandes
Actions
        ↓
Automatisation
Events + Conditions
        ↓
Communication
Network
        ↓
Exécution
Dashboard Runtime
        ↓
Affichage
Raspberry Pi + écran tactile
```

Cette architecture permet de développer progressivement Dashboard Studio sans devoir réécrire l'ensemble du projet lorsque de nouvelles fonctions seront ajoutées.

La prochaine étape technique est la création réelle de `DashboardStudio.sln`, puis la mise en place des projets et références exactement selon cette architecture.

---

**Dashboard Studio — Architecture Technique v1.0**
