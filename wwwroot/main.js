function getClients() {
    fetch('http://localhost:5199/clients')
        .then(response => response.json())
        .then(clients => {
            let clientsTable = document.getElementById('clientsTable');

            while (clientsTable.rows.length > 1) {
                clientsTable.deleteRow(1);
            }

            clients.forEach(client => {
                let row = clientsTable.insertRow();

                let nombreCell = row.insertCell();
                let edadCell = row.insertCell();
                let correoCell = row.insertCell();
                let numeroCell = row.insertCell();
                let deleteCell = row.insertCell();
                let editCell = row.insertCell();

                nombreCell.textContent = client.nombre;
                edadCell.textContent = client.edad;
                correoCell.textContent = client.correo;
                numeroCell.textContent = client.numero;

                let deleteButton = document.createElement('button');
                deleteButton.classList.add('delete-button')
                deleteButton.textContent = 'Eliminar';
                deleteButton.addEventListener('click', function () {
                    deleteClient(client.id);
                });
                deleteCell.appendChild(deleteButton);

                let editButton = document.createElement('button');
                editButton.classList.add('edit-button')
                editButton.textContent = 'Editar';
                editButton.addEventListener('click', function () {
                    editClient(client);
                });
                editCell.appendChild(editButton);
            });
        });
}

document.getElementById('clientForm').addEventListener('submit', function (event) {
    event.preventDefault();

    let nombre = document.getElementById('nombre').value;
    let edad = document.getElementById('edad').value;
    let correo = document.getElementById('correo').value;
    let numero = document.getElementById('numero').value;

    fetch('http://localhost:5199/clients', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nombre: nombre,
            edad: edad,
            correo: correo,
            numero: numero,
        }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);

            getClients();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    document.getElementById('nombre').value = '';
    document.getElementById('edad').value = '';
    document.getElementById('correo').value = '';
    document.getElementById('numero').value = '';
});

getClients();

function deleteClient(id) {
    fetch('http://localhost:5199/clients/' + id, {
        method: 'DELETE',
    })
        .then(response => {
            if (response.status === 204) {
                return {};
            } else {
                return response.json();
            }
        })
        .then(data => {
            console.log('Success:', data);
            getClients();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

let editingClient = null;

function editClient(client) {
    editingClient = client;

    document.getElementById('nombre').value = client.nombre;
    document.getElementById('edad').value = client.edad;
    document.getElementById('correo').value = client.correo;
    document.getElementById('numero').value = client.numero;
}

function addClient(event) {
    event.preventDefault();

    let nombre = document.getElementById('nombre').value;
    let edad = document.getElementById('edad').value;
    let correo = document.getElementById('correo').value;
    let numero = document.getElementById('numero').value;

    fetch('http://localhost:5199/clients', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nombre: nombre,
            edad: edad,
            correo: correo,
            numero: numero,
        }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);
            getClients();

            document.getElementById('nombre').value = '';
            document.getElementById('edad').value = '';
            document.getElementById('correo').value = '';
            document.getElementById('numero').value = '';
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

document.getElementById('clientForm').addEventListener('submit', function (event) {
    event.preventDefault();

    let nombre = document.getElementById('nombre').value.trim();
    let edad = document.getElementById('edad').value.trim();
    let correo = document.getElementById('correo').value.trim();
    let numero = document.getElementById('numero').value.trim();

    if (nombre === "" || edad === "" || correo === "" || numero === "") {
        alert('Por favor, complete todos los campos del formulario.');
        return; 
    }

    fetch('http://localhost:5199/clients', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nombre: nombre,
            edad: edad,
            correo: correo,
            numero: numero,
        }),
    })
        .then(response => response.json())
        .then(data => {
            console.log('Success:', data);

            getClients();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    document.getElementById('nombre').value = '';
    document.getElementById('edad').value = '';
    document.getElementById('correo').value = '';
    document.getElementById('numero').value = '';
});