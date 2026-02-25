process.on("uncaughtException", (err) => {
  console.error("UNCAUGHT EXCEPTION:", err);
});

process.on("unhandledRejection", (err) => {
  console.error("UNHANDLED REJECTION:", err);
});


import http from 'node:http';
import express from 'express';
import cors from 'cors';
import cookieParser from 'cookie-parser';
import bodyParser from 'body-parser';
import dotenv from 'dotenv';

import { pool } from "./config/db.mjs";
import authRoutes from "./routes/authRoutes.mjs"; 
import userRoutes from "./routes/userRoutes.mjs";

dotenv.config();

const app = express();

app.use(express.json());
app.use(bodyParser.json());
app.use(cookieParser())
app.use(cors({
    origin: process.env.FRONTEND_URL || 'http://localhost:5173',
    credentials : true
}));

const testDatabaseConnection = async()=>{
    try
    {
        const connection = await pool.getConnection();
        console.log('Connected to MySQL');
        connection.release();
    }
    catch(error)
    {
        //console.error('MySQL connection failed :', error)
    } 
}

await testDatabaseConnection();

app.get("/", (req, res) => { res.send("Backend ProPhotoStock works"); });

console.log("MOUNTING /api/auth");
app.use("/api/auth", authRoutes); 
app.use("/api/users", userRoutes);

const portHttp = process.env.PORT || 8080;

http.createServer(app).listen(portHttp, () => { 
    console.log(`🚀 Server HTTP running on http://localhost:${portHttp}`); 
});

app.post("/test-register", (req, res) => {
  console.log("HIT /test-register");
  res.json({ ok: true });
});
