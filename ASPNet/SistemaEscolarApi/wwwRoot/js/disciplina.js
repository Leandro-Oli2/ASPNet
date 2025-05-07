const apiUrl = "/api/Disciplina"; // ou http://localhost:5190/api/Disciplina

let editandoId = null;

const form = document.getElementById('disciplina-form');
const descricaoInput = document.getElementById('descricao');
const cursoInput = document.getElementById('curso');
const statusMsg = document.getElementById('status-msg');
const listaDisciplinas = document.getElementById('lista-disciplinas');

function carregarDisciplinas() {
  fetch(apiUrl)
    .then(res => res.json())
    .then(disciplinas => {
      listaDisciplinas.innerHTML = '';
      disciplinas.forEach(disciplina => {
        const div = document.createElement('div');
        div.className = 'disciplina-item';
        div.innerHTML = `
          <span><strong>Descrição:</strong> ${disciplina.descricao}</span>
          <span><strong>Curso:</strong> ${disciplina.curso}</span>
          <div class="actions">
            <button onclick="editarDisciplina(${disciplina.id})">Editar</button>
            <button class="delete" onclick="excluirDisciplina(${disciplina.id})">Excluir</button>
          </div>
        `;
        listaDisciplinas.appendChild(div);
      });
    });
}

form.addEventListener('submit', function(e) {
  e.preventDefault();

  const disciplina = {
    descricao: descricaoInput.value,
    curso: cursoInput.value
  };

  const metodo = editandoId ? 'PUT' : 'POST';
  const url = editandoId ? `${apiUrl}/${editandoId}` : apiUrl;

  fetch(url, {
    method: metodo,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(disciplina)
  })
  .then(res => {
    if (!res.ok) throw new Error('Erro ao salvar');
    return res.json();
  })
  .then(() => {
    statusMsg.textContent = editandoId ? 'Disciplina atualizada com sucesso!' : 'Disciplina cadastrada com sucesso!';
    statusMsg.style.color = 'green';
    form.reset();
    editandoId = null;
    carregarDisciplinas();
  })
  .catch(() => {
    statusMsg.textContent = 'Erro ao salvar disciplina.';
    statusMsg.style.color = 'red';
  });
});

function editarDisciplina(id) {
  fetch(`${apiUrl}/${id}`)
    .then(res => res.json())
    .then(disciplina => {
      descricaoInput.value = disciplina.descricao;
      cursoInput.value = disciplina.curso;
      editandoId = disciplina.id;
      statusMsg.textContent = 'Editando disciplina...';
      statusMsg.style.color = 'blue';
    });
}

function excluirDisciplina(id) {
  if (!confirm('Deseja realmente excluir esta disciplina?')) return;

  fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
    .then(res => {
      if (!res.ok) throw new Error('Erro ao excluir');
      carregarDisciplinas();
    })
    .catch(() => {
      alert('Erro ao excluir disciplina.');
    });
}

carregarDisciplinas();
