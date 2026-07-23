# Dashboard Studio
## Cahier des charges fonctionnel et technique

**Version :** 0.1  
**Statut :** Document de référence — conception  
**Langue :** Français  
**Dépôt GitHub :** `DashboardStudio`  
**Application de bureau :** Dashboard Studio  
**Application terminal :** Dashboard Runtime  
**Cible principale du Runtime :** Raspberry Pi avec écran tactile  
**Cas d'usage principal :** Dashboard tactile distant pour PC Windows et Microsoft Flight Simulator

---

# Historique du document

| Version | Date | Statut | Description |
|---|---|---|---|
| 0.1 | 2026 | En conception | Première spécification consolidée du projet |

---

# Table des matières

1. Présentation du projet
2. Vision et philosophie
3. Objectifs
4. Périmètre du projet
5. Utilisateurs et rôles
6. Architecture générale
7. Dashboard Studio
8. Dashboard Runtime
9. Projets, dashboards et pages
10. Système de grille
11. Responsive Dashboard
12. Widgets
13. Composants
14. Bibliothèque de composants
15. Templates
16. Data Providers
17. Actions
18. Événements et automatisations
19. Navigation tactile
20. Thèmes
21. Gestion des terminaux
22. Synchronisation Live et Draft
23. Déploiement
24. Cache et stockage local
25. Plugins
26. Intégration Windows
27. Intégration Microsoft Flight Simulator
28. Gestion des avions et configurations
29. Performances
30. Résilience et fonctionnement hors ligne
31. Sécurité
32. Gestion des erreurs
33. Sauvegarde, annulation et restauration
34. Format des projets et ressources
35. Architecture technique cible
36. Réseau et communication
37. Mise à jour et compatibilité
38. Installation et distribution
39. Tests et qualité
40. Accessibilité et ergonomie
41. Journalisation et diagnostic
42. Roadmap
43. Critères de validation
44. Principes de développement
45. Évolutions futures

---

# 1. Présentation du projet

Dashboard Studio est une plateforme logicielle destinée à créer, personnaliser, administrer et déployer des interfaces de type dashboard sur des terminaux distants.

Le projet est conçu autour de deux applications principales :

- **Dashboard Studio**, application de bureau exécutée sur le PC de l'utilisateur ;
- **Dashboard Runtime**, application légère exécutée sur un terminal distant, notamment une Raspberry Pi reliée à un écran tactile.

Le système permet à l'utilisateur de concevoir un ou plusieurs dashboards depuis son PC, puis de les afficher sur un terminal distant connecté au même réseau local.

Le cas d'utilisation initial est un écran tactile secondaire de 1280 × 800 destiné à afficher :

- des informations système du PC ;
- l'état du matériel ;
- des informations multimédias ;
- des commandes Windows ;
- des informations provenant de Microsoft Flight Simulator ;
- des commandes de simulation ;
- des interfaces de cockpit personnalisées.

Le projet doit néanmoins être conçu comme une plateforme générique. L'intégration avec Microsoft Flight Simulator constitue un cas d'utilisation prioritaire, mais l'architecture ne doit pas être limitée à ce seul logiciel.

---

# 2. Vision et philosophie

Dashboard Studio doit offrir une expérience comparable aux outils modernes de conception d'interfaces, tout en étant spécialisé dans la création d'interfaces tactiles distantes.

La philosophie du projet repose sur les principes suivants :

1. **Performance prioritaire.**
2. **Simplicité pour l'utilisateur.**
3. **Puissance par modularité.**
4. **Séparation stricte entre conception et exécution.**
5. **Bibliothèque de composants comme cœur de l'expérience de création.**
6. **Data Providers indépendants des widgets.**
7. **Actions et événements indépendants des interfaces.**
8. **Configuration minimale côté Raspberry Pi.**
9. **Déploiement automatisé.**
10. **Architecture extensible par plugins officiels.**
11. **Résilience en cas de perte réseau.**
12. **Interface moderne, claire et cohérente.**

L'objectif n'est pas de créer un simple logiciel de monitoring ou un simple panneau de commandes pour MSFS.

L'objectif est de créer une plateforme capable de transformer un écran distant en une interface personnalisée, interactive et contextuelle.

---

# 3. Objectifs

## 3.1 Objectifs fonctionnels

Dashboard Studio doit permettre de :

- créer des projets ;
- créer plusieurs dashboards ;
- créer plusieurs pages dans un dashboard ;
- ajouter des widgets ;
- déplacer les widgets ;
- redimensionner les widgets selon des contraintes prédéfinies ;
- modifier certaines propriétés visuelles ;
- utiliser des composants ;
- réutiliser des composants ;
- utiliser des templates ;
- gérer des Data Providers ;
- déclencher des actions ;
- créer des automatisations ;
- gérer plusieurs terminaux ;
- synchroniser un dashboard en temps réel ;
- publier une version stable ;
- déployer les modifications ;
- fonctionner avec un écran tactile ;
- intégrer Windows ;
- intégrer Microsoft Flight Simulator.

## 3.2 Objectifs techniques

Le système doit :

- fonctionner efficacement sur une Raspberry Pi ;
- minimiser les transferts réseau ;
- mettre en cache les ressources ;
- ne mettre à jour que les éléments modifiés lorsque cela est possible ;
- conserver un dernier état valide localement ;
- éviter les recalculs inutiles ;
- être modulaire ;
- être testable ;
- être maintenable ;
- permettre l'évolution du projet sans réécriture majeure.

---

# 4. Périmètre du projet

## 4.1 Inclus dans le périmètre initial

- Application Dashboard Studio pour Windows.
- Dashboard Runtime pour Raspberry Pi.
- Communication réseau locale.
- Détection des terminaux.
- Création et édition de dashboards.
- Gestion des pages.
- Système de grille.
- Responsive Dashboard.
- Widgets.
- Composants.
- Bibliothèque de composants.
- Templates.
- Data Providers.
- Actions.
- Événements.
- Automatisations.
- Thèmes clair et sombre pour Dashboard Studio.
- Live Mode.
- Draft Mode.
- Déploiement différentiel.
- Cache local.
- Intégration Windows.
- Intégration MSFS.
- Gestion des configurations d'avions.
- Version portable de Dashboard Studio.
- Installateur de Dashboard Studio.

## 4.2 Hors périmètre de la première version

Sont volontairement exclus de la première version :

- création de widgets par les utilisateurs ;
- création de plugins par les utilisateurs ;
- marketplace publique de plugins ;
- système de thèmes communautaires complexe ;
- historique de versions intégré au produit ;
- synchronisation cloud obligatoire ;
- gestion multi-utilisateur avancée ;
- éditeur collaboratif temps réel ;
- support officiel de multiples simulateurs dès la v1.0 ;
- application mobile dédiée.

Ces fonctionnalités pourront être étudiées ultérieurement.

---

# 5. Utilisateurs et rôles

## 5.1 Administrateur / propriétaire du projet

Le propriétaire du projet peut :

- créer des dashboards ;
- modifier les dashboards ;
- modifier les composants ;
- créer des templates ;
- configurer les terminaux ;
- configurer les plugins ;
- configurer les Data Providers ;
- publier des modifications.

## 5.2 Utilisateur final

L'utilisateur final peut :

- utiliser les dashboards ;
- déplacer certains widgets si le mode d'utilisation l'autorise ;
- redimensionner les widgets selon les contraintes ;
- modifier certaines couleurs ;
- changer de page ;
- changer de dashboard ;
- utiliser les actions ;
- utiliser les commandes tactiles.

L'utilisateur final ne peut pas :

- créer de nouveaux types de widgets ;
- créer de nouveaux plugins ;
- modifier l'architecture des widgets ;
- créer librement des Data Providers.

---

# 6. Architecture générale

Le système est composé de plusieurs couches.

```text
┌──────────────────────────────────────────┐
│             Dashboard Studio             │
│                                          │
│  UI / Designer                           │
│  Project Manager                         │
│  Component Library                       │
│  Template Manager                        │
│  Plugin Manager                          │
│  Data Provider Manager                   │
│  Action Engine                           │
│  Event Engine                            │
│  Deployment Manager                      │
│  Terminal Manager                        │
└───────────────────┬──────────────────────┘
                    │
                    │ Réseau local
                    │
                    ▼
┌──────────────────────────────────────────┐
│             Dashboard Runtime            │
│                                          │
│  Renderer                                │
│  Input / Gesture Manager                 │
│  Runtime State                           │
│  Cache                                   │
│  Action Client                           │
│  Event Client                            │
│  Synchronisation                         │
└───────────────────┬──────────────────────┘
                    │
                    ▼
             Raspberry Pi
                    │
                    ▼
             Écran tactile
```

Le système peut également communiquer avec des services externes via des plugins et Data Providers.

---

# 7. Dashboard Studio

Dashboard Studio est l'application de bureau principale.

## 7.1 Fonctions principales

L'application doit fournir :

- écran d'accueil ;
- gestionnaire de projets ;
- éditeur de dashboard ;
- bibliothèque de composants ;
- gestionnaire de templates ;
- gestionnaire de terminaux ;
- gestionnaire de plugins ;
- configuration des Data Providers ;
- gestion des actions ;
- gestion des événements ;
- paramètres ;
- diagnostic.

## 7.2 Interface

L'interface doit être :

- moderne ;
- claire ;
- cohérente ;
- responsive ;
- adaptée aux écrans Windows modernes ;
- utilisable à la souris et au clavier.

Le thème de l'application peut être :

- Clair ;
- Sombre.

Le changement de thème doit être immédiat.

## 7.3 Éditeur

L'éditeur doit permettre :

- glisser-déposer ;
- déplacement ;
- redimensionnement contraint ;
- sélection ;
- multi-sélection si pertinente ;
- duplication ;
- suppression ;
- annulation ;
- rétablissement ;
- aperçu ;
- prévisualisation ;
- publication.

---

# 8. Dashboard Runtime

Dashboard Runtime est l'application exécutée sur la Raspberry Pi.

## 8.1 Responsabilités

Le Runtime doit :

- démarrer automatiquement ;
- détecter l'écran ;
- charger le dernier dashboard valide ;
- afficher le dashboard ;
- gérer le tactile ;
- gérer les gestes ;
- recevoir les mises à jour ;
- transmettre les événements ;
- exécuter les actions demandées ;
- gérer le cache ;
- communiquer avec Dashboard Studio.

## 8.2 Principe de légèreté

Le Runtime ne doit pas embarquer inutilement les fonctions lourdes de conception.

Il ne doit pas contenir :

- l'éditeur complet ;
- la bibliothèque d'administration ;
- les outils de conception complexes ;
- les fonctions de gestion de projet non nécessaires à l'exécution.

Le Runtime doit rester spécialisé dans l'exécution.

---

# 9. Projets, dashboards et pages

## 9.1 Projet

Un projet Dashboard Studio regroupe les ressources nécessaires à une configuration.

Un projet peut contenir :

- dashboards ;
- pages ;
- composants ;
- templates ;
- assets ;
- configurations ;
- paramètres ;
- métadonnées.

## 9.2 Dashboard

Un projet peut contenir plusieurs dashboards.

Exemples :

- Desktop ;
- MSFS ;
- Monitoring ;
- Gaming ;
- Streaming.

Chaque dashboard est indépendant.

## 9.3 Pages

Un dashboard peut contenir plusieurs pages.

Exemple :

```text
MSFS
├── Waiting
├── Cockpit
├── Performance
└── Landing
```

La navigation horizontale permet de passer d'une page à une autre.

---

# 10. Système de grille

Les widgets sont positionnés sur une grille logique.

La grille :

- est adaptative ;
- conserve l'alignement ;
- permet le redimensionnement contraint ;
- s'adapte au terminal.

Les widgets ne sont pas librement positionnés au pixel près.

Chaque widget possède des dimensions autorisées.

Exemple :

```text
2 × 2
4 × 2
6 × 3
```

Les dimensions disponibles dépendent du widget.

Le système doit empêcher les tailles incompatibles.

---

# 11. Responsive Dashboard

Le système doit adapter un dashboard à différents écrans.

Les paramètres pris en compte sont :

- largeur ;
- hauteur ;
- ratio ;
- orientation ;
- densité de pixels lorsque nécessaire.

La détection automatique doit être privilégiée.

Si elle est impossible, l'utilisateur peut renseigner les informations nécessaires.

La configuration doit rester simple.

Le Runtime doit utiliser les informations disponibles pour calculer la grille et la mise en page.

---

# 12. Widgets

Un widget est une unité fonctionnelle affichée dans un dashboard.

Exemples :

- CPU ;
- GPU ;
- RAM ;
- température ;
- graphique ;
- jauge ;
- musique ;
- météo ;
- avion ;
- altitude ;
- vitesse ;
- bouton de commande.

## 12.1 Création

Les utilisateurs ne créent pas de nouveaux types de widgets.

Les widgets sont fournis par :

- le cœur de Dashboard Studio ;
- les plugins officiels.

## 12.2 Personnalisation

Selon le widget, l'utilisateur peut :

- déplacer ;
- redimensionner ;
- modifier certaines couleurs ;
- modifier certaines propriétés visuelles ;
- modifier les options exposées.

Le widget conserve son identité fonctionnelle.

Un widget CPU ne devient pas un widget GPU simplement par modification de configuration.

## 12.3 Interactions

Un widget peut réagir à :

- appui ;
- double appui ;
- appui long ;
- maintien ;
- glissement ;
- rotation virtuelle.

Les interactions disponibles sont définies par le widget.

---

# 13. Composants

Un composant est une unité réutilisable composée de plusieurs éléments.

Exemple :

```text
Engine Panel
├── RPM
├── Oil Temperature
├── Oil Pressure
├── Fuel Flow
└── Background
```

Un composant peut être :

- inséré ;
- déplacé ;
- redimensionné ;
- réutilisé ;
- personnalisé selon ses propriétés exposées.

Les composants sont au cœur de la bibliothèque de Dashboard Studio.

---

# 14. Bibliothèque de composants

La bibliothèque est l'espace central de création.

Elle doit proposer :

- catégories ;
- recherche ;
- favoris ;
- éléments récents ;
- aperçu ;
- glisser-déposer.

Catégories possibles :

- Système ;
- Médias ;
- Monitoring ;
- MSFS ;
- Contrôles ;
- Navigation ;
- Instruments ;
- Panneaux.

Les utilisateurs peuvent utiliser les composants fournis.

La création de nouveaux types de composants reste réservée au système de développement officiel.

---

# 15. Templates

Un template est un modèle préconçu.

Un template peut représenter :

- une page ;
- un dashboard ;
- une structure de dashboard.

Exemples :

- Desktop ;
- Monitoring ;
- MSFS ;
- Cockpit ;
- Gaming.

Un template peut être copié et personnalisé.

La copie d'une configuration doit être possible.

Le système doit privilégier la réutilisation plutôt que la duplication manuelle.

---

# 16. Data Providers

Les Data Providers constituent une abstraction entre les widgets et les sources de données.

Architecture :

```text
Widget
  ↓
Data Contract
  ↓
Data Provider
  ↓
Source externe
```

Exemples :

```text
System.CPU.Usage
System.GPU.Usage
System.Memory.Usage
MSFS.Aircraft.Altitude
MSFS.Aircraft.IndicatedAirspeed
MSFS.Aircraft.Heading
Media.CurrentTrack
```

Le widget ne doit pas dépendre directement de l'application externe.

## 16.1 Avantages

Cette architecture permet :

- remplacement d'une source ;
- ajout de nouvelles sources ;
- tests indépendants ;
- réutilisation des widgets ;
- séparation des responsabilités.

---

# 17. Actions

Les actions permettent d'envoyer des commandes.

Exemples :

- lancer une application ;
- modifier le volume ;
- contrôler les médias ;
- exécuter une commande ;
- commander MSFS.

Un widget déclenche une action.

L'action est ensuite exécutée par le service approprié.

```text
Interaction
↓
Action
↓
Action Provider
↓
Système cible
```

Une interaction peut déclencher plusieurs actions.

Exemple :

```text
Mode Vol
├── Ouvrir MSFS
├── Régler le volume
├── Changer le dashboard
└── Régler la luminosité
```

---

# 18. Événements et automatisations

Le système doit pouvoir réagir à des événements.

Exemples :

- MSFS lancé ;
- MSFS fermé ;
- vol chargé ;
- avion détecté ;
- avion changé ;
- connexion perdue ;
- connexion rétablie.

Exemple :

```text
MSFS lancé
↓
Dashboard MSFS
↓
Page Waiting
```

Puis :

```text
Vol chargé
↓
Détection de l'avion
↓
Chargement du setup correspondant
```

Une automatisation peut exécuter plusieurs actions.

---

# 19. Navigation tactile

## 19.1 Horizontal

Balayer vers la gauche ou la droite :

- change de page dans le dashboard actif.

## 19.2 Vers le bas

Ouvre le Centre de contrôle.

Le Centre de contrôle peut afficher :

- volume ;
- luminosité ;
- état réseau ;
- terminal ;
- synchronisation ;
- paramètres.

## 19.3 Vers le haut

Ouvre le Launcher des dashboards.

Le Launcher permet de sélectionner un dashboard.

La liste principale doit rester compacte.

Le nombre total de dashboards installés est limité artificiellement à trois.

Le nombre de favoris affichés simultanément peut être limité pour préserver la lisibilité.

## 19.4 Navigation automatique

Le système peut changer automatiquement de dashboard selon les événements.

Exemple :

```text
MSFS fermé
↓
Desktop

MSFS lancé
↓
MSFS / Waiting

Vol chargé
↓
MSFS / Cockpit
```

---

# 20. Thèmes

Dashboard Studio propose deux thèmes officiels :

- Clair ;
- Sombre.

Le changement est immédiat.

Les dashboards ont leur propre apparence.

Le thème de Dashboard Studio ne force pas automatiquement le changement du thème d'un dashboard.

---

# 21. Gestion des terminaux

Un terminal est un appareil exécutant Dashboard Runtime.

Le terminal doit pouvoir être découvert automatiquement sur le réseau local.

Informations :

- nom ;
- modèle ;
- résolution ;
- ratio ;
- orientation ;
- version Runtime ;
- état ;
- connexion.

L'utilisateur doit pouvoir :

- renommer ;
- sélectionner ;
- associer un dashboard ;
- publier ;
- surveiller l'état.

La configuration réseau doit être aussi automatique que possible.

---

# 22. Synchronisation Live et Draft

## 22.1 Live

En Live Mode :

- les modifications sont transmises immédiatement ;
- le Runtime affiche les changements ;
- seules les ressources nécessaires sont transférées.

## 22.2 Draft

En Draft Mode :

- les modifications restent sur le PC ;
- le Runtime conserve la version publiée ;
- la publication est volontaire.

La séparation Live/Draft doit être clairement visible dans l'interface.

---

# 23. Déploiement

Deux modes sont nécessaires.

## 23.1 Déploiement différentiel

Le système compare l'état local et distant.

Seuls les éléments différents sont envoyés.

## 23.2 Déploiement complet

L'utilisateur peut forcer une resynchronisation complète.

Ce mode sert notamment à :

- réparer un état incohérent ;
- restaurer un terminal ;
- reconstruire le cache.

---

# 24. Cache et stockage local

Le Runtime doit conserver localement :

- dashboard actif ;
- pages ;
- images ;
- icônes ;
- polices ;
- composants ;
- ressources nécessaires.

Le cache doit être persistant.

Le Runtime doit pouvoir démarrer sans connexion au PC avec une configuration par défaut de type réveil.

La taille du cache doit être contrôlée.

Les ressources inutilisées peuvent être supprimées selon une stratégie définie.

---

# 25. Plugins

Les plugins permettent d'étendre le système.

Ils peuvent fournir :

- widgets ;
- Data Providers ;
- actions ;
- événements ;
- intégrations.

Les utilisateurs finaux ne créent pas de plugins.

Les plugins sont maintenus par les développeurs officiels.

Chaque plugin doit déclarer :

- identité ;
- version ;
- compatibilité ;
- dépendances ;
- capacités.

Le système doit refuser ou isoler un plugin incompatible.

---

# 26. Intégration Windows

Le système Windows doit pouvoir fournir :

- CPU ;
- GPU ;
- mémoire ;
- stockage ;
- réseau ;
- températures disponibles ;
- volume ;
- médias ;
- état des applications.

Actions possibles :

- lancer une application ;
- fermer une application lorsque cela est autorisé ;
- modifier le volume ;
- contrôler les médias ;
- exécuter une commande autorisée.

Les fonctions nécessitant des privilèges élevés doivent être explicitement contrôlées.

---

# 27. Intégration Microsoft Flight Simulator

MSFS constitue l'intégration prioritaire.

Le système doit pouvoir :

- détecter MSFS ;
- détecter le début et la fin d'une session ;
- détecter le chargement d'un vol ;
- identifier l'appareil lorsque les données disponibles le permettent ;
- récupérer des données de simulation ;
- envoyer des commandes compatibles.

Données potentielles :

- altitude ;
- vitesse ;
- vitesse verticale ;
- cap ;
- position ;
- carburant ;
- moteurs ;
- températures ;
- pressions ;
- systèmes ;
- autopilot ;
- radio ;
- transpondeur.

Commandes potentielles :

- train ;
- parking brake ;
- flaps ;
- autopilot ;
- heading ;
- altitude ;
- COM ;
- NAV ;
- transpondeur ;
- lumières.

L'intégration doit être conçue de manière modulaire afin de pouvoir évoluer avec MSFS.

---

# 28. Gestion des avions et configurations

Lorsqu'un vol est chargé, le système doit tenter d'identifier l'appareil.

Le système peut alors sélectionner une configuration associée.

Exemple :

```text
MSFS
↓
Avion détecté
↓
Airbus A320
↓
Configuration A320
↓
Dashboard Cockpit
```

La configuration peut définir :

- dashboard ;
- page de départ ;
- composants ;
- commandes ;
- événements ;
- automatisations.

Si aucun setup spécifique n'est disponible, le système utilise un setup générique.

---

# 29. Performances

Les performances sont une priorité absolue.

## 29.1 Dashboard Studio

Objectifs :

- interface fluide ;
- manipulation à 60 FPS lorsque le matériel le permet ;
- démarrage rapide ;
- ouverture rapide des projets ;
- synchronisation réactive.

## 29.2 Runtime

Objectifs :

- faible utilisation CPU ;
- mémoire maîtrisée ;
- rendu fluide ;
- absence de recalcul global inutile.

Lorsqu'un widget change :

```text
Mauvais :
Widget modifié
↓
Reconstruction complète du dashboard

Objectif :
Widget modifié
↓
Mise à jour ciblée
```

## 29.3 Réseau

Les transferts doivent être minimisés.

Le système doit éviter de retransférer les ressources identiques.

---

# 30. Résilience et fonctionnement hors ligne

Le Runtime doit être résilient.

En cas de perte du réseau :

- une page par défaut de type réveil est affichée;
- l'état de connexion est clairement indiqué.

Lorsque le réseau revient :

- le Runtime se reconnecte automatiquement ;
- l'état est vérifié ;
- le dashboard est de nouveau affiché;
- les modifications manquantes sont synchronisées.

Une perte réseau ne doit pas provoquer un écran vide.

---

# 31. Sécurité

Les communications doivent être authentifiées lorsque cela est nécessaire.

Le Runtime doit accepter les commandes provenant uniquement de sources autorisées.

Les opérations critiques doivent demander confirmation.

Exemples :

- suppression d'un dashboard ;
- suppression d'une page ;
- suppression d'un composant ;
- suppression d'un terminal.

Les opérations non destructives ne demandent pas de confirmation.

---

# 32. Gestion des erreurs

Le système doit fournir des erreurs compréhensibles.

Exemples :

```text
Terminal introuvable
Connexion perdue
Déploiement interrompu
Plugin incompatible
Ressource manquante
Data Provider indisponible
```

Les erreurs doivent :

- être journalisées ;
- être affichées clairement ;
- ne pas faire planter toute l'application lorsque cela peut être évité.

---

# 33. Sauvegarde, annulation et restauration

Pendant l'édition :

- Ctrl+Z annule ;
- Ctrl+Y rétablit.

L'historique d'annulation est conservé pendant la session d'édition.

Les opérations critiques demandent confirmation.

Le projet doit être sauvegardé automatiquement selon une stratégie définie.

Le projet ne doit pas implémenter dans la v1.0 un système complet d'historique utilisateur comparable à Git.

Git reste le système de versionnement du code et des fichiers du projet.

---

# 34. Format des projets et ressources

Les formats internes doivent être :

- lisibles ;
- versionnables ;
- extensibles ;
- validables.

Les fichiers de configuration doivent privilégier des formats structurés.

Les ressources binaires doivent être séparées des définitions.

Architecture logique :

```text
Projet
├── Métadonnées
├── Dashboards
│   ├── Pages
│   │   └── Widgets
│   ├── Composants
│   └── Configurations
├── Templates
├── Assets
└── Plugins
```

Une version de schéma doit être enregistrée afin de permettre les migrations futures.

---

# 35. Architecture technique cible

L'architecture exacte sera définie dans le document d'architecture technique.

La solution devra séparer au minimum :

```text
DashboardStudio.UI
DashboardStudio.Core
DashboardStudio.Models
DashboardStudio.Network
DashboardStudio.Runtime
DashboardStudio.Plugins
DashboardStudio.Windows
DashboardStudio.MSFS
DashboardStudio.Tests
```

Cette liste constitue une proposition initiale et pourra être ajustée après étude technique.

Les responsabilités doivent être séparées.

Le code UI ne doit pas contenir directement la logique réseau ou les accès MSFS.

---

# 36. Réseau et communication

La communication PC ↔ Runtime doit fonctionner sur le réseau local.

Le système doit privilégier :

- découverte automatique ;
- connexion persistante lorsque nécessaire ;
- échanges événementiels ;
- messages structurés ;
- synchronisation différentielle.

Le protocole exact sera choisi pendant la phase d'architecture technique.

Il devra prendre en compte :

- latence ;
- fiabilité ;
- sécurité ;
- simplicité ;
- performance sur Raspberry Pi.

---

# 37. Mise à jour et compatibilité

Le Runtime doit pouvoir être mis à jour.

Dashboard Studio doit connaître la version du Runtime.

Une incompatibilité doit être signalée clairement.

Le système doit éviter qu'un déploiement incompatible rende le terminal inutilisable.

Une stratégie de rollback du Runtime pourra être ajoutée dans une version ultérieure.

---

# 38. Installation et distribution

Dashboard Studio doit être distribué sous deux formes :

## Version portable

Une archive ou un dossier autonome permettant d'exécuter Dashboard Studio sans installation classique.

## Installateur

Un installateur Windows permettant :

- installation ;
- raccourcis ;
- désinstallation ;
- association éventuelle de fichiers ;
- mises à jour futures.

Le Runtime sera fourni séparément pour Raspberry Pi.

---

# 39. Tests et qualité

Le projet doit intégrer des tests.

## Tests unitaires

À utiliser pour :

- logique métier ;
- modèles ;
- validation ;
- calcul de grille ;
- synchronisation ;
- sérialisation.

## Tests d'intégration

À utiliser pour :

- réseau ;
- Data Providers ;
- plugins ;
- MSFS ;
- déploiement.

## Tests de performance

À réaliser sur :

- PC cible ;
- Raspberry Pi cible ;
- réseau Wi-Fi.

## Tests de résilience

Tester :

- perte réseau ;
- redémarrage Runtime ;
- arrêt brutal ;
- ressources manquantes ;
- données indisponibles.

---

# 40. Accessibilité et ergonomie

L'interface doit respecter :

- tailles de texte lisibles ;
- contrastes suffisants ;
- zones tactiles suffisamment grandes ;
- retours visuels ;
- animations modérées.

Les interactions tactiles doivent être conçues pour éviter les erreurs.

Les gestes doivent être désactivables ou configurables si nécessaire.

---

# 41. Journalisation et diagnostic

Dashboard Studio et Dashboard Runtime doivent posséder un système de logs.

Les logs doivent permettre de diagnostiquer :

- connexion ;
- synchronisation ;
- déploiement ;
- plugins ;
- Data Providers ;
- erreurs Runtime.

Les logs doivent pouvoir être exportés pour faciliter le support.

Le niveau de journalisation doit être configurable.

---

# 42. Roadmap

## Phase 0 — Conception

- Cahier des charges.
- Architecture technique.
- Architecture réseau.
- Choix technologiques.
- Maquettes UI/UX.
- Formats de fichiers.

## Phase 1 — Prototype Runtime

Objectif :

Afficher un dashboard statique sur Raspberry Pi.

Fonctions :

- démarrage ;
- connexion réseau ;
- rendu ;
- grille ;
- widgets simples.

## Phase 2 — Dashboard Studio

Fonctions :

- interface ;
- projet ;
- éditeur ;
- pages ;
- widgets ;
- sauvegarde.

## Phase 3 — Communication

Fonctions :

- détection Runtime ;
- synchronisation ;
- Live Mode ;
- Draft Mode ;
- cache.

## Phase 4 — Bibliothèque

Fonctions :

- composants ;
- templates ;
- recherche ;
- glisser-déposer.

## Phase 5 — Data Providers et Actions

Fonctions :

- architecture Data Provider ;
- Windows Provider ;
- actions ;
- événements.

## Phase 6 — MSFS

Fonctions :

- détection ;
- données ;
- avion ;
- setups ;
- commandes.

## Phase 7 — Automatisation

Fonctions :

- événements ;
- changement automatique de dashboard ;
- navigation tactile ;
- Centre de contrôle ;
- Launcher.

## Phase 8 — Optimisation

Fonctions :

- performances ;
- mémoire ;
- réseau ;
- stabilité.

## Phase 9 — Version 1.0

Fonctions :

- portable ;
- installateur ;
- Runtime stable ;
- documentation ;
- tests finaux.

---

# 43. Critères de validation

## Dashboard Studio

- [ ] L'application démarre.
- [ ] Un projet peut être créé.
- [ ] Un dashboard peut être créé.
- [ ] Plusieurs pages peuvent être créées.
- [ ] Les widgets peuvent être ajoutés.
- [ ] Les widgets peuvent être déplacés.
- [ ] Les widgets peuvent être redimensionnés selon leurs contraintes.
- [ ] Les composants peuvent être utilisés.
- [ ] Les templates peuvent être utilisés.
- [ ] Les projets peuvent être sauvegardés.
- [ ] Undo/Redo fonctionne.
- [ ] Les thèmes clair et sombre fonctionnent.

## Runtime

- [ ] Le Runtime démarre.
- [ ] Le terminal est détectable.
- [ ] Un dashboard est affiché.
- [ ] Les pages sont accessibles.
- [ ] Les gestes fonctionnent.
- [ ] Le cache fonctionne.
- [ ] Le Runtime démarre sans réseau avec une configuration valide.
- [ ] Le Runtime se reconnecte automatiquement.

## Synchronisation

- [ ] Live Mode fonctionne.
- [ ] Draft Mode fonctionne.
- [ ] Déploiement différentiel fonctionne.
- [ ] Déploiement complet fonctionne.

## Windows

- [ ] Les données système sont disponibles.
- [ ] Les actions Windows fonctionnent.

## MSFS

- [ ] MSFS est détecté.
- [ ] Les données sont récupérées.
- [ ] L'avion est détecté lorsque possible.
- [ ] Le setup correspondant peut être sélectionné.
- [ ] Les actions compatibles fonctionnent.

## Performance

- [ ] Dashboard Studio reste fluide.
- [ ] Runtime reste stable.
- [ ] La consommation de ressources est maîtrisée.
- [ ] Les mises à jour ciblées fonctionnent.

---

# 44. Principes de développement

Le développement doit suivre les règles suivantes :

1. Ne pas ajouter une fonctionnalité majeure sans la documenter.
2. Ne pas mélanger UI, logique métier et communication.
3. Ne pas dépendre directement d'un service externe depuis un widget.
4. Utiliser les Data Providers.
5. Utiliser les Actions.
6. Préserver les performances.
7. Tester les composants critiques.
8. Prévoir l'évolution des formats.
9. Ne pas ajouter de complexité inutile.
10. Préférer une architecture simple et extensible.

Chaque modification importante doit être :

- documentée ;
- testée ;
- versionnée dans Git.

---

# 45. Évolutions futures

Les fonctionnalités suivantes pourront être étudiées après la v1.0 :

- marketplace de plugins ;
- plugins tiers contrôlés ;
- synchronisation cloud ;
- multi-utilisateur ;
- collaboration ;
- historique interne avancé ;
- système de thèmes personnalisés ;
- support de plusieurs simulateurs ;
- application mobile ;
- gestion de plusieurs Raspberry Pi ;
- dashboards synchronisés ;
- mode multi-écran ;
- API publique ;
- SDK développeur.

Ces fonctionnalités ne doivent pas compromettre la simplicité et les performances du produit principal.

---

# Conclusion

Dashboard Studio est conçu comme une plateforme modulaire de création d'interfaces tactiles et interactives.

Le projet repose sur une séparation claire entre :

- conception ;
- exécution ;
- données ;
- actions ;
- événements ;
- plugins ;
- déploiement.

Dashboard Studio constitue l'environnement de création et d'administration.

Dashboard Runtime constitue l'environnement d'exécution léger.

Les Data Providers assurent l'accès aux données.

Les Actions assurent les commandes.

Les Événements et Automatisations assurent la logique réactive.

Les Widgets et Composants assurent la présentation.

Les Templates accélèrent la création.

Le système de déploiement assure la synchronisation entre le PC et les terminaux.

La priorité de la première version est de construire une base fiable, performante, maintenable et extensible.

Le projet doit être développé progressivement.

Aucune fonctionnalité majeure ne doit être implémentée sans avoir été préalablement définie et validée.

---

**Dashboard Studio — Cahier des charges v0.1**

**Fin du document**
