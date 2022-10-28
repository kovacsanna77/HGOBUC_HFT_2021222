fetch('http://localhost:27826/Movie')
    .then(x => x.json())
    .then(y => console.log(y));