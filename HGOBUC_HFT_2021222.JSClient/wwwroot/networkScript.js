﻿let networks = [];
let connection = null;
let networkIdtoupdate = -1;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:27826/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("NetworkCreated", (user, message) => {
        getdata();
    });

    connection.on("NetworkDeleted", (user, message) => {
        getdata();
    });
    connection.on("NetworkUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
  
    await fetch('http://localhost:27826/Network')
        .then(x => x.json())
        .then(y => {
            networks = y;
           
           display();
        });
   
}

function display() {
   
    document.getElementById('resultarea').innerHTML = '';
    networks.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.networkId + "</td>"
            + "<td>" + t.networkName + "</td><td>"
        + `<button type="button" onclick="remove(${t.networkId})">Delete</button>`
        + `<button type="button" onclick="ShowUpdate(${t.networkId})">Update</button>`
            + "</td></tr>";
    });
}

function ShowUpdate(id) {
    document.getElementById('networktoUpdate').value = networks.find(t => t.networkId == id)['networkName'];
    document.getElementById('updateformdiv').style.display = 'inline';
   networkIdtoupdate = id;
}
function update() {

    let name = document.getElementById('networktoUpdate').value;
    document.getElementById('updateformdiv').style.display = 'none';

    fetch('http://localhost:27826/Network/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { networkName: name, networkId: networkIdtoupdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
           
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
    
}
function remove(id) {
    fetch('http://localhost:27826/Network/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(networks.length);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
   
}

function create() {
    let name = document.getElementById('networkname').value;
    fetch('http://localhost:27826/Network', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { networkName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            console.log(networks.length);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}