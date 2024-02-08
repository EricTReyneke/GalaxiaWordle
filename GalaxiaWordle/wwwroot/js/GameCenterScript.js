function LetterInputOnClick(rowNumber, letterNumber, wordLength) {
    document
        .getElementById(`row-${rowNumber}-letter-${letterNumber}`)
        .setAttribute('readonly', true);

    if (letterNumber == wordLength && rowNumber == wordLength) {
        CheckWordleWord(rowNumber);
        return;
    }
    else if (letterNumber == wordLength) {
        CheckWordleWord(rowNumber, wordLength);
        rowNumber++;
        letterNumber = 1;
    }
    else {
        letterNumber++;
    }

    var nextInput = document.getElementById(`row-${rowNumber}-letter-${letterNumber}`);
    nextInput.removeAttribute('readonly');
    nextInput.focus();
}

function CheckWordleWord(rowNumber, wordLength) {
    var fullWord = FindFullWord(rowNumber, wordLength);

    ValidateWordleWord(fullWord)
        .then(function (wordDictionary) {
            if (wordDictionary != null) {
                DisplayWordleWordAccuracy(wordDictionary, rowNumber);
            }
        })
        .catch(function (error) {
            console.error("An error occurred:", error);
        });
}

const FindFullWord = (rowNumber, wordLength) =>
    Array.from({ length: wordLength }, (_, i) => i + 1)
        .map(i => document.getElementById(`row-${rowNumber}-letter-${i}`).value)
        .join('');

function ValidateWordleWord(wordleWord) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/GameCenter?handler=ValidateWordleWord",
            headers: {
                "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
            },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: { wordleWord: wordleWord },
            success: function (response) {
                console.log(response);
                resolve(response);
            },
            error: function (error) {
                console.log(error);
                reject(error);
            }
        });
    });
}

function DisplayWordleWordAccuracy(wordDictionary, rowNumber) {
    var greenLetters = 0;
    var keys = Object.keys(wordDictionary);
    var values = Object.values(wordDictionary);

    for (var i = 0; i < keys.length; i++) {
        if (values[i] == "Green") {
            document.getElementById(`row-${rowNumber}-letter-${i + 1}`).style.backgroundColor = '#76AF54';
            greenLetters++;
        }
        else if (values[i] == "Yellow") {
            document.getElementById(`row-${rowNumber}-letter-${i + 1}`).style.backgroundColor = '#ffe79a';
        }
    }

    if (greenLetters == 5) {
        ShowCongratulationsPopUp();
    }
}

function ShowCongratulationsPopUp() {
    document.querySelector('.CongratulationsPopUp').style.display = 'flex';
}

function HideCongratulationsPopUp() {
    document.querySelector('.CongratulationsPopUp').style.display = 'none';
}

function HandleSelectChange(selectElement) {
    var selectedValue = selectElement.value;

    $.ajax({
        type: "POST",
        url: "/GameCenter?handler=SetWordLength",
        headers: {
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        },
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: { wordLength: selectedValue },
        success: function (response) {
            if (response === "Success.") {
                DisplayWordleGame();
                console.log('Word length set successfully.');
            } else {
                console.error('Failed to set word length.');
            }
        },
        error: function (error) {
            console.error('Error sending data:', error);
        }
    });

    updateWordleGame(selectedValue);
}

function updateWordleGame(wordLength) {
    fetch(`/GameCenter?handler=WordleGame&wordLength=${wordLength}`)
        .then(response => response.text())
        .then(html => {
            document.querySelector('.wordle-partial').innerHTML = html;
        })
        .catch(error => console.error('Error loading new wordle game:', error));
}

//Style functions
function ShowWordleSelectBox() {
    // Hide the wordle card
    var wordleCard = document.getElementById("wordle-game-card");
    if (wordleCard) {
        wordleCard.style.setProperty("display", "none", "important");
    }

    var numberSelectInfo = document.getElementById("number-select-info");
    if (numberSelectInfo) {
        numberSelectInfo.style.setProperty("display", "block", "important");
    }

    var numberSelect = document.getElementById("number-select");
    if (numberSelect) {
        numberSelect.style.setProperty("display", "block", "important");
    }
}

function DisplayWordleGame() {

    // Display the wordle game as flex
    var wordleGame = document.getElementById("wordle-game-div");
    if (wordleGame) {
        wordleGame.style.setProperty("display", "flex", "important");
    }
}