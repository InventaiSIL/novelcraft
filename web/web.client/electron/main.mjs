import { app, BrowserWindow } from 'electron';
import path from 'path';
import { fileURLToPath } from 'url';

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

function createWindow() {
  const win = new BrowserWindow({ width: 800, height: 600 });

  win.loadFile(path.join(__dirname, '../dist/index.html')); // chemin relatif vers le HTML
}

app.whenReady().then(createWindow);
