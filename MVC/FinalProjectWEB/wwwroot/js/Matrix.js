const canvas = document.getElementById('matrixCanvas');
const ctx = canvas.getContext('2d');

const matrixSize = 1000;
const canvasSize = 1000; // גודל הקנבס ב-pixels
const cellSize = canvasSize / matrixSize;

let agentsList, targetsList;

getData();
let agents = [

];

let targets = [

];
fillLists();
function fillLists() {
    setInterval(async () => {
        agents = [];
        targets = [];
        await getData();
        agentsList.forEach(a => {
            agents.push({ x: a.locationX, y: a.locationY });
        });
        console.log(agents);
        targetsList.forEach(t => {
            if (t.status != 1) {
                targets.push({ x: t.locationX, y: t.locationY });
            }
        });
        console.log(target);
    }, 5000);
}

// פונקציה לציור המטריצה והאובייקטים
function drawMatrix() {
    ctx.clearRect(0, 0, canvas.width, canvas.height); // מנקה את הקנבס

    // צייר סוכנים
    ctx.fillStyle = 'blue';
    agents.forEach(agent => {
        ctx.fillRect(agent.x * cellSize, agent.y * cellSize, 5, 5);
    });

    // צייר מטרות
    ctx.fillStyle = 'red';
    targets.forEach(target => {
        ctx.fillRect(target.x * cellSize, target.y * cellSize, 5, 5);
    });
}


// עדכון הציור כל 100ms
setInterval(() => {
    drawMatrix();
}, 100);




async function getData() {
    let url = "http://localhost:5008/agents";
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const json = await response.json();
        agentsList = json;
    } catch (error) {
        console.error(error.message);
    }

    url = "http://localhost:5008/targets";
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const json = await response.json();
        targetsList = json;
    } catch (error) {
        console.error(error.message);
    }
}