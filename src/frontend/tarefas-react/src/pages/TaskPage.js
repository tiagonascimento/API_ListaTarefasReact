import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Form, Input, message, Spin } from 'antd';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const TaskPage = () => {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [modalVisible, setModalVisible] = useState(false);
  const navigate = useNavigate();
  const token = localStorage.getItem('token');

  const fetchTasks = async () => {
    setLoading(true);
    try {
      const res = await axios.get('http://localhost/api/Task', {
        headers: { Authorization: `Bearer ${token}` },
      });
      setTasks(Array.isArray(res.data) ? res.data : []);
    } catch (err) {
      if (err.response?.status === 401) {
        message.error('Sessão expirada');
        localStorage.removeItem('token');
        navigate('/');
      } else {
        message.error('Erro ao carregar tarefas');
      }
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const handleDelete = async (id) => {
    try {
      await axios.delete(`http://localhost/api/Task/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      message.success('Tarefa excluída com sucesso');
      fetchTasks();
    } catch {
      message.error('Erro ao excluir tarefa');
    }
  };

  const handleCreate = async (values) => {
    try {
      await axios.post('http://localhost/api/Task', {
        title: values.title,
        description: values.description,
      }, {
        headers: { Authorization: `Bearer ${token}` },
      });
      message.success('Tarefa criada com sucesso');
      setModalVisible(false);
      fetchTasks();
    } catch {
      message.error('Erro ao criar tarefa');
    }
  };

  const handleEdit = async (record) => {
    try {
      await axios.put(`http://localhost/api/Task/${record.id}`, {}, {
        headers: { Authorization: `Bearer ${token}` },
      });
      message.success('Tarefa atualizada com sucesso');
      fetchTasks();
    } catch {
      message.error('Erro ao atualizar tarefa');
    }
  };

  const columns = [
    { title: 'Título', dataIndex: 'title', key: 'title' },
    { title: 'Descrição', dataIndex: 'description', key: 'description' },
    {
      title: 'Criado em',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (value) => new Date(value).toLocaleString('pt-BR'),
    },
    { title: 'Status', dataIndex: 'status', key: 'status' },
    {
      title: 'Ações',
      render: (_, record) => (
        <>
          <Button type="link" onClick={() => handleEdit(record)}>
            Editar
          </Button>
          <Button danger type="link" onClick={() => handleDelete(record.id)}>
            Excluir
          </Button>
        </>
      ),
    },
  ];

  return (
    <Spin spinning={loading}>
      <Button type="primary" onClick={() => setModalVisible(true)} style={{ margin: 16 }}>
        Nova Tarefa
      </Button>

      <Table dataSource={tasks} columns={columns} rowKey="id" />

      <Modal
        open={modalVisible}
        onCancel={() => setModalVisible(false)}
        footer={null}
        title="Nova Tarefa"
      >
        <Form layout="vertical" onFinish={handleCreate}>
          <Form.Item name="title" label="Título" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Form.Item name="description" label="Descrição" rules={[{ required: true }]}>
            <Input />
          </Form.Item>
          <Button type="primary" htmlType="submit" block>
            Criar Tarefa
          </Button>
        </Form>
      </Modal>
    </Spin>
  );
};

export default TaskPage;