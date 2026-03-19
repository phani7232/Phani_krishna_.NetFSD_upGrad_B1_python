function calculateGrade(percent) {
    if (percent >= 80) return "A";
    else if (percent >= 50) return "B";
    else return "Fail";
}

function calcPercentage(score, total) {
    return (score / total) * 100;
}

function isPass(percent) {
    return percent >= 50;
}

// TESTS
test("Grade Calculation", () => {
    expect(calculateGrade(85)).toBe("A");
});

test("Percentage Calculation", () => {
    expect(calcPercentage(2, 4)).toBe(50);
});

test("Pass/Fail", () => {
    expect(isPass(40)).toBe(false);
});