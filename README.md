# Projet Intégrateur

## Présentation

<div align="justify"> Bienvenu sur notre projet qui a été fait dans le cadre du cours “Projet Intégrateur” à l’Université de Strasbourg dans l’année scolaire 2021/2022 au dernier semestre de notre licence d’Informatique. Nous sommes 14 étudiants à avoir travaillé ensemble à sa réalisation : tout nos noms sont disponibles dans la rubrique "Membres". Ce projet a pour but de recréer entièrement un jeu de société en mode plateau sur une application téléchargeable sur ordinateur ou via un accès internet. Nous avons choisi le jeu Carcassonne qui peut se jouer de deux à cinq joueurs.</div>

## Branches

Nous avons plusieurs branches:<br/>
- La branche **main** est administrative. Elle contient tout les documents que nous avons rédigés.
- La branche **develop** est la branche principale contenant tout notre projet.

## Savoir utiliser Git

Instructions à suivre pour développer une nouvelle implémentation sur git dans la bonne humeur.

### 1. Récupérer le plus récent commit

```bash
git checkout develop ; git pull
```

- **checkout** => changer de branche
- **pull** => récupérer le plus récent commit

### 2. Créer sa propre branche

```bash
git checkout -b <nom_de_la_branche>
```

- **-b** => créer une nouvelle branche et se positionner dessus

### 3. Etre libre de ses modifications

```bash
git add * ; git commit -m <message>
```

- **add** => rajoute les fichiers dans l'index (à commiter)
- **commit** => applique les modifications en local

### 4. Push sur le repositoire distant (optionnel)

```bash
git push --set-upstream origin <nom_de_la_branche>
```

- **push** => charge le contenu d'un dépôt local vers un dépôt distant

### 5. Merge sur develop

```bash
git checkout develop ; git merge <nom_de_la_branche>
```

- **merge** => fusionne une branche avec une autre

### 6. Informations diverses

Supprimer une branche:
```bash
git branch -d <nom_de_la_branche>
```

Gérer un conflit:
```bash
git diff
```
