import express from "express";
import { auth } from "../middleware/auth.mjs";
import { requireRole } from "../middleware/role.mjs";

const router = express.Router();

//Admin
router.get("/admin", auth, requireRole("admin"), (req, res) => {
  res.json({ message: "admin" });
});

// Photograph
router.get("/photographer", auth, requireRole("photographer"), (req, res) => {
  res.json({ message: "Photographer" });
});

// Clientes
router.get("/client", auth, requireRole("client"), (req, res) => {
  res.json({ message: "Client" });
});

export default router;
