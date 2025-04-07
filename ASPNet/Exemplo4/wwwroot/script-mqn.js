const API_MAQUINA = "http://localhost:5000/Maquina";

document.getElementById("maquinaform").addEventListener("submit", salvarMaquina);
carregarMaquinas();

function carregarMaquinas() {
    fetch(API_MAQUINA)
        .then(res => res.json())
        .then(data => {
            const tbody = document.querySelector("#tabelaMaquina tbody");
            tbody.innerHTML = "";
            data.forEach(maquina => {
                tbody.innerHTML += `
                    <tr>
                        <td>${maquina.id_maquina}</td>
                        <td>${maquina.tipo}</td>
                        <td>${maquina.velocidade}</td>
                        <td>${maquina.hardDisk}</td>
                        <td>${maquina.memoria}</td>
                        <td>
                            <button class="edit" onclick="editarMaquina(${maquina.id_maquina})">Editar</button>
                            <button class="delete" onclick="deletarMaquina(${maquina.id_maquina})">Deletar</button>
                        </td>
                    </tr>
                `;
            });
        })
        .catch(error => console.error("Erro ao carregar máquinas:", error));
}

function salvarMaquina(event) {
    event.preventDefault();
    console.log("Formulário submetido");  // Log para ver se a função está sendo chamada

    const id = document.getElementById("id").value;
    const maquina = {
        id_maquina: id ? parseInt(id) : 0,
        tipo: document.getElementById("tipo").value,
        velocidade: parseInt(document.getElementById("velocidade").value),
        harddisk: parseInt(document.getElementById("harddisk").value),
        placa_rede: parseInt(document.getElementById("placarede").value),
        memoria_ram: parseInt(document.getElementById("memoriaram").value),
        fk_usuario: parseInt(document.getElementById("fkusuario").value)
    };    
    console.log("Dados enviados:", maquina);  // Log para ver se os dados estão sendo passados corretamente

    const metodo = id ? "PUT" : "POST";
    const url = id ? `${API_MAQUINA}/${id}` : API_MAQUINA;

    fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(maquina)
    })
        .then(res => res.json())
        .then(() => {
            document.getElementById("maquinaform").reset();
            carregarMaquinas();
        })
        .catch(error => console.error("Erro ao salvar:", error));
}
function editarMaquina(id) {
    fetch(`${API_MAQUINA}/${id}`)
        .then(res => res.json())
        .then(maquina => {
            document.getElementById("id_maquina").value = maquina.id_maquina;
            document.getElementById("tipo").value = maquina.tipo;
            document.getElementById("velocidade").value = maquina.velocidade;
            document.getElementById("harddisk").value = maquina.hardDisk;
            document.getElementById("placarede").value = maquina.placa;
            document.getElementById("memoriaram").value = maquina.memoria;
            document.getElementById("fkusuario").value = maquina.fkUsuario;
        })
        .catch(error => console.error("Erro ao carregar dados para edição:", error));
}

function deletarMaquina(id) {
    if (confirm("Deseja realmente excluir esta máquina?")) {
        fetch(`${API_MAQUINA}/${id}`, { method: "DELETE" })
            .then(res => {
                if (!res.ok) throw new Error("Erro ao deletar");
                carregarMaquinas();
            })
            .catch(error => console.error("Erro ao excluir:", error));
    }
}
