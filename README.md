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


