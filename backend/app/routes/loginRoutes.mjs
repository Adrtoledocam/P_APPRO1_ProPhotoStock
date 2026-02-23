import { Router } from "express";
import { login } from "../controllers/loginController.mjs";

const router = Router();

router.post("/", login);

export default router;
