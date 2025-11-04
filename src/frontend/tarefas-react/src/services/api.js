import axios from 'axios';

const api = axios.create({
  baseURL: '/api/v1', // garante que todas as chamadas vão para /api/v1
});

const handleLogin = async () => {
  try {
    const response = await api.post('/login', {
      id: 0,
      name: 'string', // substitua por nome real, se necessário
      email: 'teste@teste.com',
      password: '123456',
    });

    console.log('Login bem-sucedido:', response.data);
    // Aqui você pode salvar o token, redirecionar, etc.
  } catch (error) {
    console.error('Erro ao fazer login:', error.response?.data || error.message);
  }
};
