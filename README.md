# ProPhotoStock

ProPhotoStock est une application mobile cross-plateforme dédiée à la gestion et à la vente de licences photographiques. Elle connecte les photographes professionnels et les clients via une interface intuitive, un backend robuste sous Docker et un stockage cloud.

---

## 🚀 Installation et Déploiement

### 1. Prérequis système
- Docker Desktop (v4.0+)
- Visual Studio 2022 avec le workload .NET MAUI
- Git ou GitHub Desktop
- Un émulateur Android configuré

---

### 2. Récupération du projet
```bash
git clone https://github.com/votre-utilisateur/ProPhotoStock.git
cd ProPhotoStock
```

---

### 3. Déploiement du Backend (Docker)
```bash
# Placez-vous dans le dossier Backend (si applicable)
cd Backend

# Construisez et lancez les services en arrière-plan
docker-compose up -d --build

# Vérifiez l'état des conteneurs (optionnel)
docker ps
```

---

### 4. Configuration du Frontend
```csharp
// Exemple : ApiService.cs
public static string BaseUrl = "http://10.0.2.2:3000/api/";
```

---

### 5. Lancement
```md
# Ouvrez la solution .sln dans Visual Studio.
# Sélectionnez l'émulateur Android comme cible.
# Lancez le débogage (F5).
```

---

## 📱 Manuel d'Utilisation

### 🔐 Gestion du compte
- **Inscription** : Saisissez vos informations et choisissez le rôle "Client" ou "Photographe".
- **Connexion** : Accès sécurisé via Token JWT.

---

### 👤 Guide Client
- **Catalogue** : Parcourez la galerie et cliquez sur une photo pour l'acheter.
- **Achat** : Sélectionnez le contrat (Exclusif ou Diffusion), précisez l'usage et confirmez.
- **Mes Contrats** : Retrouvez vos photos acquises dans l'onglet dédié.

---

### 📷 Guide Photographe
- **Partage** : Utilisez l'onglet d'upload (Caméra ou Galerie).
- **Publication** : Définissez le titre et la catégorie pour envoyer l'image vers Cloudinary.
- **Suivi** : Consultez l'onglet "Contrats" pour visualiser l'historique des gains.

---

### 🛠 Guide Administrateur
- Interface identique pour une navigation simplifiée.
- **Rapport Mensuel** : Dans la section "Contrats", l'administrateur accède à une vue globale pour superviser l'activité de la plateforme.

---

## 🛠 Technologies utilisées
- **Frontend** : .NET MAUI (C# / XAML)
- **Backend** : Node.js / Express
- **Base de données** : MySQL
- **Infrastructure** : Docker & Docker Compose
- **Stockage Cloud** : Cloudinary
- **Sécurité** : Authentification JWT

---
