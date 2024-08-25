const express = require('express');
const path = require('path');
const compression  = require('compression');
const app = express();

app.use(compression());

const headersForStatic = {
  setHeaders: (res, path) => {
  	console.log("_j path:", path);
    
    if (path.endsWith('.gz')) {
      res.set('Content-Encoding', 'gzip');
    }
    if (path.endsWith('.data.gz')) {
        res.set('Content-Type', 'application/gzip');
    }
    if (path.endsWith('.wasm.gz')) {
        res.set('Content-Type', 'application/wasm');
    }
    if (path.endsWith('.js.gz')) {
        res.set('Content-Type', 'application/javascript');
    }
    if (path.endsWith('.symbols.json.gz')) {
        res.set('Content-Type', 'application/octet-stream');
    }
    
    if (path.endsWith('.br')) {
      res.set('Content-Encoding', 'br');
    }
    if (path.endsWith('.data.br')) {
    	res.set('Content-Type', 'application/octet-stream');
    }
    if (path.endsWith('.wasm.br')) {
    	res.set('Content-Type', 'application/wasm');
    }
    if (path.endsWith('.js.br')) {
    	res.set('Content-Type', 'application/javascript');
    }
    if (path.endsWith('.symbols.json.br')) {
    	res.set('Content-Type', 'application/octet-stream');
    }

    if (path.endsWith('.wasm')) {
        res.set('Content-Type', 'application/wasm');
    }
  }
};
app.use(express.static(path.join(__dirname, '..', 'Builds', 'webgl'), headersForStatic));

app.get('/qwe', (req, res) => {
	res.send('qwe');
});

app.listen(8000, () => {
	console.log('Server has started!')
});
