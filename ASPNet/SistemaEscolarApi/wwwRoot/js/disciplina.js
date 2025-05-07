const apiUrl = "/api/Disciplina"; // ou http://localhost:5190/api/Aluno

let editandoId = null;

const form = document.getElementById('aluno-form');
const nomeInput = document.getElementById('nome');
const cursoInput = document.getElementById('curso');
const statusMsg = document.getElementById('status-msg');
const listaAlunos = document.getElementById('lista-alunos');

function carregarAlunos() {
  fetch(apiUrl)
    .then(res => res.json())
    .then(alunos => {
      listaAlunos.innerHTML = '';
      alunos.forEach(aluno => {
        const div = document.createElement('div');
        div.className = 'aluno-item';
        div.innerHTML = `
          <span><strong>Nome:</strong> ${aluno.nome}</span>
          <span><strong>Curso:</strong> ${aluno.curso}</span>
          <div class="actions">
            <button onclick="editarAluno(${aluno.id})">Editar</button>
            <button class="delete" onclick="excluirAluno(${aluno.id})">Excluir</button>
          </div>
        `;
        listaAlunos.appendChild(div);
      });
    });
}

form.addEventListener('submit', function(e) {
  e.preventDefault();

  const aluno = {
    nome: nomeInput.value,
    curso: cursoInput.value
  };

  const metodo = editandoId ? 'PUT' : 'POST';
  const url = editandoId ? `${apiUrl}/${editandoId}` : apiUrl;

  fetch(url, {
    method: metodo,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(aluno)
  })
  .then(res => {
    if (!res.ok) throw new Error('Erro ao salvar');
    return res.json();
  })
  .then(() => {
    statusMsg.textContent = editandoId ? 'Aluno atualizado com sucesso!' : 'Aluno cadastrado com sucesso!';
    statusMsg.style.color = 'green';
    form.reset();
    editandoId = null;
    carregarAlunos();
  })
  .catch(() => {
    statusMsg.textContent = 'Erro ao salvar aluno.';
    statusMsg.style.color = 'red';
  });
});

function editarAluno(id) {
  fetch(`${apiUrl}/${id}`)
    .then(res => res.json())
    .then(aluno => {
      nomeInput.value = aluno.nome;
      cursoInput.value = aluno.curso;
      editandoId = aluno.id;
      statusMsg.textContent = 'Editando aluno...';
      statusMsg.style.color = 'blue';
    });
}

function excluirAluno(id) {
  if (!confirm('Deseja realmente excluir este aluno?')) return;

  fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
    .then(res => {
      if (!res.ok) throw new Error('Erro ao excluir');
      carregarAlunos();
    })
    .catch(() => {
      alert('Erro ao excluir aluno.');
    });
}

carregarAlunos();