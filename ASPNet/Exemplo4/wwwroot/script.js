//vou criar uma variavel que vai receber o endereço da aplicação ASP.Net
const API = "http://localhost:5000/Usuario";


//Atribuindo os valores dos campos do formuladrio para um objeto

document.getElementById("usuarioForm").addEventListener("submit", salvarUsuario);
document.addEventListener("DOMContentLoaded", carregarUsuarios);


function carregarUsuarios(){
    fetch(API).then(res => res.json())

    .then(data =>{
        const tbody = document.querySelector("#tableUsuarios tbody");

        tbody.innerHTML = "";

        data.forEach(usuario =>{
            tbody.innerHTML += `
                <tr>
                    <td>${usuario.id}</td>
                    <td>${usuario.nome}</td>
                    <td>${usuario.senha}</td>
                    <td>${usuario.ramal}</td>
                    <td>${usuario.especialidade}</td>
                    <td>
                        <button class="edit" onclick="editarUsuario(${usuario.id})">Editar</button>
                        <button class="delete" onclick='deletarUsuario(${usuario.id})'>Deletar</button>
                    </td>
                </tr>
            `;
        })
    })
}