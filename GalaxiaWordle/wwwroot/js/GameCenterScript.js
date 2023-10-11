function LetterInputOnClick(rowNumber, letterNumber) {
    document
        .getElementById(`row-${rowNumber}-letter-${letterNumber}`)
        .setAttribute('readonly', true);

    if (letterNumber == 5 && rowNumber == 5) {
        CheckWordleWord(rowNumber);
        return;
    }
    else if (letterNumber == 5) {
        CheckWordleWord(rowNumber);
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

function CheckWordleWord(rowNumber) {
    var fullWord = FindFullWord(rowNumber);

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

function FinalWrodleWordCheck() {
    var fullWord = FindFullWord(5);
}

const FindFullWord = rowNumber =>
    Array.from({ length: 5 }, (_, i) => i + 1)
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