let movies = [];

fetch('http://localhost:27826/Movie')
    .then(x => x.json())
    .then(y => {
        movies = y;
        console.log(movies);
        display();
    });

function display() {
    movies.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.movieId + "</td><td>" + t.title + "</td><td>" + t.aired + "</td><td>"
            + t.episodes + "</td><td>" + t.duration + "</td><td>" + t.network.networkName + "</td><td>" + t.rating;
        
    })
}