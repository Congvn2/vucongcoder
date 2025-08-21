const canvas = document.getElementById("flappyCanvas");
const ctx = canvas.getContext("2d");

let gameWidth = window.innerWidth;
let gameHeight = window.innerHeight;
canvas.width = gameWidth;
canvas.height = gameHeight;

let bird = { x: gameWidth / 4, y: gameHeight / 2, w: 40, h: 30, dy: 0 };
let pipes = [];
let frame = 0;
let pipeGap = gameHeight * 0.25;
let gameOver = false;
let score = 0;

function flapBird() {
    if (!gameOver) bird.dy = -7;
}

function restartGame() {
    bird.y = gameHeight / 2; bird.dy = 0;
    pipes = []; frame = 0; gameOver = false; score = 0;
    document.getElementById("bigscore").innerText = score;
    document.getElementById("replay").style.display = "none";
    loop();
}

document.addEventListener("keydown", e => { if (e.code === "Space") flapBird(); });
document.addEventListener("touchstart", e => { e.preventDefault(); flapBird(); });

window.addEventListener("resize", () => {
    gameWidth = window.innerWidth;
    gameHeight = window.innerHeight;
    canvas.width = gameWidth;
    canvas.height = gameHeight;
});

function update() {
    if (gameOver) return;

    bird.dy += 0.3;
    bird.y += bird.dy;

    if (frame % 90 === 0) {
        let top = Math.random() * (gameHeight - pipeGap - 60) + 30;
        let bottom = gameHeight - top - pipeGap;
        pipes.push({ x: gameWidth, top: top, bottom: bottom, passed: false });
    }

    pipes.forEach(p => p.x -= 3);

    pipes = pipes.filter(p => p.x + 50 > 0);

    for (let p of pipes) {
        if (bird.x < p.x + 50 && bird.x + bird.w > p.x &&
            (bird.y < p.top || bird.y + bird.h > gameHeight - p.bottom)) {
            gameOver = true;
        }
        if (!p.passed && p.x + 50 < bird.x) {
            score++; p.passed = true;
            document.getElementById("bigscore").innerText = score;
        }
    }

    if (bird.y + bird.h > gameHeight || bird.y < 0) gameOver = true;

    if (gameOver) {
        document.getElementById("replay").style.display = "block";
    }
}

function draw() {
    ctx.clearRect(0, 0, gameWidth, gameHeight);
    ctx.fillStyle = "#ff0";
    ctx.fillRect(bird.x, bird.y, bird.w, bird.h);
    ctx.fillStyle = "#0f0";
    pipes.forEach(p => {
        ctx.fillRect(p.x, 0, 50, p.top);
        ctx.fillRect(p.x, gameHeight - p.bottom, 50, p.bottom);
    });
}

function loop() {
    update();
    draw();
    frame++;
    if (!gameOver) requestAnimationFrame(loop);
}

loop();
