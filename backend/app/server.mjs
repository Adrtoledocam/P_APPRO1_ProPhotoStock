import http from 'node:http';
import express from 'express';
import cors from 'cors';
import cookieParser from 'cookie-parser';
import bodyParser from 'body-parser';
import dotenv from 'dotenv';

import { databaseConnection } from "./config/db.mjs";

//Routes
//import userRoutes from './routes/userRoutes.mjs';
//import photoRoutes from './routes/photoRoutes.mjs';
//import contractRoutes from './routes/contractRoutes.mjs';
import loginRoutes from './routes/loginRoutes.mjs';

dotenv.config();

const app = express();

app.use(express.json());
app.use(bodyParser.json());
app.use(cookieParser())
app.use(cors({
    origin: process.env.FRONTEND_URL || 'http://localhost:5173',
    credentials : true
}));

app.use(databaseConnection);

//app.use('/api/users', userRoutes); 
//app.use('/api/photos', photoRoutes); 
//app.use('/api/contracts', contractRoutes);
app.use('/api/login', loginRoutes);

const portHttp = process.env.PORT || 8080;

http.createServer(app).listen(portHttp, () => { 
    console.log(`🚀 Server HTTP running on http://localhost:${portHttp}`); 
});