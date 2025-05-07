const apiUrl = "/api/Curso"; // 

let editandoId = null;

const form = document.getElementById('curso-form');
const descricaoInput = document.getElementById('descricao');
const statusMsg = document.getElementById('status-msg');
const listaCursos = document.getElementById('lista-cursos');

function carregarCursos() {
  fetch(apiUrl)
    .then(res => res.json())
    .then(cursos => {
      listaCursos.innerHTML = '';
      cursos.forEach(curso => {
        const div = document.createElement('div');
        div.className = 'curso-item d-flex justify-content-between align-items-center';
        div.innerHTML = `
          <span><strong>Descrição:</strong> ${curso.descricao}</span>
          <div class="actions">
            <button class="btn btn-sm btn-warning" onclick="editarCurso(${curso.id})">
              <i class="fas fa-edit"></i> Editar
            </button>
            <button class="btn btn-sm btn-danger" onclick="excluirCurso(${curso.id})">
              <i class="fas fa-trash-alt"></i> Excluir
            </button>
          </div>
        `;
        listaCursos.appendChild(div);
      });
    });
}

form.addEventListener('submit', function(e) {
  e.preventDefault();

  const curso = {
    descricao: descricaoInput.value
  };

  const metodo = editandoId ? 'PUT' : 'POST';
  const url = editandoId ? `${apiUrl}/${editandoId}` : apiUrl;

  fetch(url, {
    method: metodo,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(curso)
  })
  .then(async res => {
    if (!res.ok) throw new Error('Erro ao salvar');
    const text = await res.text();
    return text ? JSON.parse(text) : {};
  })
  
  .then(() => {
    statusMsg.textContent = editandoId ? 'Curso atualizado com sucesso!' : 'Curso cadastrado com sucesso!';
    statusMsg.style.color = 'green';
    form.reset();
    editandoId = null;
    carregarCursos();
  })
  .catch(() => {
    statusMsg.textContent = 'Erro ao salvar curso.';
    statusMsg.style.color = 'red';
  });
});

function editarCurso(id) {
  fetch(`${apiUrl}/${id}`)
    .then(res => res.json())
    .then(curso => {
      descricaoInput.value = curso.descricao;
      editandoId = curso.id;
      statusMsg.textContent = 'Editando curso...';
      statusMsg.style.color = 'blue';
    });
}

function excluirCurso(id) {
  if (!confirm('Deseja realmente excluir este curso?')) return;

  fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
    .then(res => {
      if (!res.ok) throw new Error('Erro ao excluir');
      carregarCursos();
    })
    .catch(() => {
      alert('Erro ao excluir curso.');
    });
}

carregarCursos();
