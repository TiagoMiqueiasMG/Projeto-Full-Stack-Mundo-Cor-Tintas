import React, { useState, useEffect } from "react";
import "./Fretes.css";
import axios from "axios";
import { getJwtToken } from "../../../lib/authUtils";
import FreteModal from "../../../components/FreteModal";

const Fretes = () => {
  const [entregas, setEntregas] = useState([]);
  const [selectedRows, setSelectedRows] = useState([]);
  const [selectAll, setSelectAll] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [modalData, setModalData] = useState(null);
  const [filtroNome, setFiltroNome] = useState("");
  const [filtroData, setFiltroData] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const jwtToken = await getJwtToken();
        const response = await axios.get(
          "https://localhost:7192/fretes/listar",
          {
            headers: {
              Authorization: `Bearer ${jwtToken}`,
            },
          }
        );
        setEntregas(response.data);
      } catch (error) {
        console.error("Erro ao obter dados da API", error);
      }
    };

    fetchData();
  }, []);

  const handleCheckboxChange = (id) => {
    setSelectedRows((prevSelectedRows) =>
      prevSelectedRows.includes(id)
        ? prevSelectedRows.filter((rowId) => rowId !== id)
        : [...prevSelectedRows, id]
    );
  };

  const handleSelectAllChange = () => {
    if (selectAll) {
      setSelectedRows([]);
    } else {
      setSelectedRows(entregas.map((entrega) => entrega.id));
    }
    setSelectAll(!selectAll);
  };

  const handleEditar = () => {
    if (selectedRows.length === 1) {
      const selectedId = selectedRows[0];
      const selectedItem = entregas.find(
        (entrega) => entrega.id === selectedId
      );
      setModalData(selectedItem);
      setModalOpen(true);
    } else {
      console.warn("Selecione apenas um item para editar.");
    }
  };

  const excluirFrete = async (id) => {
    try {
      const jwtToken = await getJwtToken();
      await axios.delete("https://localhost:7192/fretes/deletar", {
        headers: {
          Authorization: `Bearer ${jwtToken}`,
        },
        data: { id },
      });

      const updatedEntregas = entregas.filter((entrega) => entrega.id !== id);
      setEntregas(updatedEntregas);
      setSelectedRows([]);
    } catch (error) {
      console.error("Erro ao excluir item", error);
    }
  };

  const handleExcluir = () => {
    selectedRows.forEach((id) => {
      excluirFrete(id);
    });
  };

  const atualizarFrete = async (updatedData) => {
    try {
      const jwtToken = await getJwtToken();
      await axios.put("https://localhost:7192/fretes/atualizar", updatedData, {
        headers: {
          Authorization: `Bearer ${jwtToken}`,
        },
      });

      const updatedEntregas = entregas.map((entrega) =>
        entrega.id === updatedData.id ? updatedData : entrega
      );
      setEntregas(updatedEntregas);
    } catch (error) {
      console.error("Erro ao atualizar frete", error);
    }
  };

  const formatarData = (data) => {
    const dataFormatada = new Date(data);
    const dia = dataFormatada.getDate();
    const mes = dataFormatada.getMonth() + 1;
    const ano = dataFormatada.getFullYear();

    return `${dia}/${mes < 10 ? "0" : ""}${mes}/${ano}`;
  };

  const filtrarLista = () => {
    let listaFiltrada = entregas;

    if (filtroNome) {
      listaFiltrada = listaFiltrada.filter((entrega) =>
        entrega.nomeCliente.toLowerCase().includes(filtroNome.toLowerCase())
      );
    }

    if (filtroData) {
      listaFiltrada = listaFiltrada.filter((entrega) =>
        formatarData(entrega.dataEntrega)
          .toLowerCase()
          .includes(filtroData.toLowerCase())
      );
    }

    return listaFiltrada;
  };

  return (
    <div>
      <div className="filtro-container">
        <input
          type="text"
          placeholder="FILTRAR POR NOME"
          value={filtroNome}
          onChange={(e) => setFiltroNome(e.target.value)}
        />
        <input
          type="text"
          placeholder="FILTRAR POR DATA"
          value={filtroData}
          onChange={(e) => setFiltroData(e.target.value)}
        />
        <button onClick={filtrarLista}>Filtrar</button>
      </div>
      <div className="body-container-frete">
        <table className="custom-table">
          <thead>
            <tr>
              <th>Cliente</th>
              <th>CEP</th>
              <th>Rua</th>
              <th>Número</th>
              <th>Data de Entrega</th>
              <th>Valor do Frete</th>
              <th>Valor da Compra</th>
              <th>
                <div className="select-header">
                  <input
                    type="checkbox"
                    checked={selectAll}
                    onChange={handleSelectAllChange}
                  />
                  <label>Selecionar Itens</label>
                </div>
              </th>
            </tr>
          </thead>
          <tbody>
            {filtrarLista().map((entrega) => (
              <tr key={entrega.id}>
                <td>{entrega.nomeCliente}</td>
                <td>{entrega.cep}</td>
                <td>{entrega.rua}</td>
                <td>{entrega.numero}</td>
                <td>{formatarData(entrega.dataEntrega)}</td>
                <td>{entrega.valorFrete}</td>
                <td>{entrega.valorCompra}</td>
                <td>
                  <input
                    type="checkbox"
                    checked={selectedRows.includes(entrega.id)}
                    onChange={() => handleCheckboxChange(entrega.id)}
                  />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="action-buttons">
        <button onClick={handleEditar} className="edit-button">
          Editar
        </button>
        {modalOpen && (
          <FreteModal
            data={modalData}
            onClose={() => setModalOpen(false)}
            onUpdate={atualizarFrete}
          />
        )}
        <button onClick={handleExcluir} className="delete-button">
          Excluir
        </button>
      </div>
    </div>
  );
};

export default Fretes;
