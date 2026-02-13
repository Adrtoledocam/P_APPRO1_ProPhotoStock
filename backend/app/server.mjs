import http from 'http'; 
const server = http.createServer((req, res) => { 
    res.end('Node.js - Docker'); 
}); 
server.listen(8080, () => { 
    console.log('8080'); 
});