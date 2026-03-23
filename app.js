const courses = [
    { id: 1, name: "HTML", lessons: ["Intro", "Tags"] },
    { id: 2, name: "CSS", lessons: ["Flexbox", "Grid"] },
    { id: 3, name: "JavaScript", lessons: ["DOM", "Events"] },
    { id: 4, name: "Bootstrap", lessons:["Pagination","Grids"]}
];

const quizData = [
    {
        q: "What is HTML?",
        options: ["Markup", "Programming", "Database"],
        answer: "Markup"
    },
    {
        q: "CSS stands for?",
        options: ["Style Sheets", "Script Sheets", "None"],
        answer: "Style Sheets"
    },
    {
        q:"Js is used for?",
        options:["Styling","Dynamic and Interactive","Both"],
        answer:"Dynamic and Interactive"
    }
];


function showToast(msg) {
    let toast = document.createElement("div");
    toast.className = "toast";
    toast.innerText = msg;
    document.body.appendChild(toast);

    toast.style.display = "block";
    setTimeout(() => toast.remove(), 2000);
}

/* DASHBOARD */
if (document.getElementById("progressBar")) {
    let completed = JSON.parse(localStorage.getItem("completed")) || [];
    let percent = (completed.length / courses.length) * 100;

    let bar = document.getElementById("progressBar");
    let value = 0;

    let interval = setInterval(() => {
        if (value >= percent) clearInterval(interval);
        value++;
        bar.value = value;
    }, 20);

    document.getElementById("totalCourses").innerText = courses.length;
    document.getElementById("completedCourses").innerText = completed.length;
}
if (document.getElementById("recentQuiz")) {
    let scores = JSON.parse(localStorage.getItem("quizScores")) || [];

    if (scores.length > 0) {
        let latest = scores[scores.length - 1];
        let best = Math.max(...scores.map(s => s.score));

        document.getElementById("recentQuiz").innerHTML = `
            Latest: ${latest.score}% <br>
            Best: ${best}%
        `;
    } else {
        document.getElementById("recentQuiz").innerText = "No quiz attempts yet";
    }
}

/* COURSES */
document.addEventListener("DOMContentLoaded", () => {

    const courses = [
        { id: 1, name: "HTML", lessons: ["Intro", "Tags"] },
        { id: 2, name: "CSS", lessons: ["Flexbox", "Grid"] },
        { id: 3, name: "JavaScript", lessons: ["DOM", "Events"] },
        { id: 4, name: "Bootstrap", lessons:["Pagination","Grids"]}
    ];

    const table = document.getElementById("courseTable");

    if (table) {
        table.innerHTML = "";

        courses.forEach(c => {
            const row = document.createElement("tr");

            row.innerHTML = `
    <td>${c.name}</td>
    <td>
        <ol>${c.lessons.map(l => `<li>${l}</li>`).join("")}</ol>
    </td>
    <td>
        <button class="${isCompleted(c.id) ? 'done' : ''}"
        onclick="toggleComplete(${c.id}, this)">
        ${isCompleted(c.id) ? "Completed" : "Complete"}
        </button>
    </td>
`;

            table.appendChild(row);
        });
    }

});
function isCompleted(id) {
    let completed = JSON.parse(localStorage.getItem("completed")) || [];
    return completed.includes(id);
}
function toggleComplete(id, btn) {
    let completed = JSON.parse(localStorage.getItem("completed")) || [];

    if (completed.includes(id)) {
        // REMOVE (undo)
        completed = completed.filter(c => c !== id);
        btn.innerText = "Complete";
        btn.classList.remove("done");
        showToast("Course Marked Incomplete 🔄");
    } else {
        // ADD
        completed.push(id);
        btn.innerText = "Completed";
        btn.classList.add("done");
        showToast("Course Completed ✅");
    }

    localStorage.setItem("completed", JSON.stringify(completed));

    // OPTIONAL: update dashboard instantly
    updateProgress();
}

/* QUIZ */
async function loadQuiz() {
    return new Promise(resolve => {
        setTimeout(() => resolve(quizData), 1200);
    });
}

let answers = {};

if (document.getElementById("quizContainer")) {
    loadQuiz().then(data => {
        let html = "";

        data.forEach((q, i) => {
            html += `
            <div class="quiz-card">
                <h4>${q.q}</h4>
            `;

            q.options.forEach(opt => {
                html += `
                <label class="option">
                    <input type="radio" name="q${i}" value="${opt}"
                    onchange="selectAnswer(${i}, '${opt}')">
                    <span>${opt}</span>
                </label>
                `;
            });

            html += `</div>`;
        });

        document.getElementById("quizContainer").innerHTML = html;
    });
}
function selectAnswer(i, val) {
    answers[i] = val;
}

function submitQuiz() {
    let score = 0;

    quizData.forEach((q, i) => {
        if (answers[i] === q.answer) score++;
    });

    let percent = Math.round((score / quizData.length) * 100);

    // GET OLD SCORES
    let scores = JSON.parse(localStorage.getItem("quizScores")) || [];

    // ADD NEW SCORE WITH DATE
    let newAttempt = {
        score: percent,
        date: new Date().toLocaleString()
    };

    scores.push(newAttempt);

    // SAVE BACK
    localStorage.setItem("quizScores", JSON.stringify(scores));

    // ALSO KEEP LATEST (for quick use)
    localStorage.setItem("score", percent);

    let grade = percent >= 80 ? "A" : percent >= 50 ? "B" : "Fail";

    let message;
    switch (grade) {
        case "A": message = " Excellent"; break;
        case "B": message = " Good Job"; break;
        default: message = " Try Again";
    }

    document.getElementById("result").innerHTML = `
       <div class="card result-card ${grade}">
        <h1>${percent}%</h1>
        <p>Grade: ${grade}</p>
        <p>${message}</p>
    </div>
    `;
}

/* PROFILE */
if (document.getElementById("completedList")) {
    let completed = JSON.parse(localStorage.getItem("completed")) || [];
    let ul = document.getElementById("completedList");

    completed.forEach(id => {
        let c = courses.find(x => x.id === id);
        let li = document.createElement("li");
        li.innerText = c.name;
        ul.appendChild(li);
    });

    document.getElementById("lastScore").innerText =
        localStorage.getItem("score") || "No attempt";
}
if (document.getElementById("progressBar")) {
    let completed = JSON.parse(localStorage.getItem("completed")) || [];

let percent = 0;

if (completed.length > 0) {
    percent = ((completed.length / courses.length) * 100);
}

document.getElementById("progressBar").value = percent;

    document.getElementById("totalCourses").innerText = courses.length;
    document.getElementById("completedCourses").innerText = completed.length;
    document.getElementById("progressBar").value = percent;

    document.getElementById("progressText").innerText =
        `You completed ${percent}% of courses`;

    document.getElementById("recentQuiz").innerText =
        localStorage.getItem("score")
        ? `Last Score: ${localStorage.getItem("score")}%`
        : "No quiz attempted yet";
}
if (document.getElementById("quizHistory")) {
    let scores = JSON.parse(localStorage.getItem("quizScores")) || [];
    let ul = document.getElementById("quizHistory");

    if (scores.length === 0) {
        ul.innerHTML = "<li>No attempts yet</li>";
    } else {
        scores.reverse().forEach(s => {
            let li = document.createElement("li");
            li.innerText = `${s.score}% - ${s.date}`;
            ul.appendChild(li);
        });
    }
}
if (document.getElementById("totalCoursesProfile")) {

    const courses = [
        { id: 1, name: "HTML" },
        { id: 2, name: "CSS" },
        { id: 3, name: "JavaScript" },
        { id: 4, name: "Bootstrap" }
    ];

    let completed = JSON.parse(localStorage.getItem("completed")) || [];
    let scores = JSON.parse(localStorage.getItem("quizScores")) || [];

    // BASIC DATA
    document.getElementById("totalCoursesProfile").innerText = courses.length;
    document.getElementById("completedProfile").innerText = completed.length;
    // SCORES
    if (scores.length > 0) {
        let latest = scores[scores.length - 1].score;
        let best = Math.max(...scores.map(s => s.score));

        document.getElementById("latestScore").innerText = latest + "%";
        document.getElementById("bestScore").innerText = best + "%";
    } else {
        document.getElementById("latestScore").innerText = "No data";
        document.getElementById("bestScore").innerText = "No data";
    }
}
// SAVE PROFILE
function saveProfile() {
    const name = document.getElementById("nameInput").value;
    const email = document.getElementById("emailInput").value;
    const phone = document.getElementById("phoneInput").value;

    // SAVE DATA
    const user = { name, email, phone };
    localStorage.setItem("userProfile", JSON.stringify(user));

    // CLEAR FIELDS
    document.getElementById("nameInput").value = "";
    document.getElementById("emailInput").value = "";
    document.getElementById("phoneInput").value = "";

    // SHOW SUCCESS MESSAGE
    showToast("Saved successfully");
}

// LOAD PROFILE
document.addEventListener("DOMContentLoaded", () => {
    const user = JSON.parse(localStorage.getItem("userProfile"));

    if (user) {
        if (document.getElementById("nameInput")) {
            document.getElementById("nameInput").value = user.name || "";
            document.getElementById("emailInput").value = user.email || "";
            document.getElementById("phoneInput").value = user.phone || "";
        }
    }
});
// SHOW NAME IN DASHBOARD
if (document.getElementById("welcomeText")) {
    const user = JSON.parse(localStorage.getItem("userProfile"));

    if (user && user.name) {
        document.getElementById("welcomeText").innerText =
            `Welcome back, ${user.name} `;
    } else {
        document.getElementById("welcomeText").innerText =
            "Welcome back, Guest";
    }
}
