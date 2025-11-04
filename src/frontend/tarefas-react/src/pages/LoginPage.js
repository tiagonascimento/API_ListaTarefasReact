// src/pages/LoginPage.js
import React, { useState } from 'react';
import { Form, Input, Button, message } from 'antd';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const FIXED_USER = 'teste@teste.com';
const FIXED_PASS = '123456';

const LoginPage = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const onFinish = async () => {
    setLoading(true);
    try {
        const res = await axios.post('http://localhost:80/api/v1/login', {
          id: 0,
          name: 'string', // ou o nome real do usuário, se necessário
          email: FIXED_USER,
          password: FIXED_PASS,
        });
      localStorage.setItem('token', res.data.token);
      navigate('/tasks');
    } catch (err) {
      message.error('Erro ao autenticar com a API');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Form
      onFinish={onFinish}
      layout="vertical"
      style={{ maxWidth: 300, margin: '100px auto' }}
    >
      <Form.Item label="Email">
        <Input value={FIXED_USER} disabled />
      </Form.Item>
      <Form.Item label="Senha">
        <Input.Password value={FIXED_PASS} disabled />
      </Form.Item>
      <Button type="primary" htmlType="submit" loading={loading} block>
        Entrar
      </Button>
    </Form>
  );
};

export default LoginPage;