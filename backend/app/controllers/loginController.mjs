// app/controllers/loginController.mjs
import { hashPassword, comparePassword } from '../tools/hash.mjs';
import { createToken } from '../tools/createToken.mjs';

export const login = async (req, res) => {
  const { email, motDePasse } = req.body;

  if (!email || !motDePasse) {
    return res.status(400).json({ message: "Email et mot de passe requis" });
  }

  try {
    //USER
    const [rows] = await req.db.query(
      "SELECT * FROM Utilisateur WHERE email = ?",
      [email]
    );

    if (rows.length === 0) {
      return res.status(401).json({ message: "Identifiants invalides" });
    }

    const user = rows[0];

    //PASSWORD
    const isValidPassword = comparePassword(motDePasse, user.motDePasse);

    if (!isValidPassword) {
      return res.status(401).json({ message: "Identifiants invalides" });
    }

    //TOKEN
    const token = createToken({
      id: user.Id_Utilisateur,
      role: user.role,
      email: user.email
    });

    res.cookie("token", token, {
      httpOnly: true,
      secure: false,
      sameSite: "lax"
    });

    return res.json({
      message: "Connexion réussie",
      user: {
        id: user.Id_Utilisateur,
        nom: user.nom,
        email: user.email,
        role: user.role
      }
    });

  } catch (error) {
    console.error("Erreur login:", error);
    return res.status(500).json({ message: "Erreur serveur" });
  }
};
