
import React, { useState, useEffect } from "react";
import "./Calculadora.css";
import Mapa from "../../../components/map";
import api from "../../../lib/axios";
import { getAuth, onAuthStateChanged } from "firebase/auth";

const Calculadora = () => {

  const [jwtToken, setJwtToken] = useState("");
  
  const [dadosEnvio, setDadosEnvio] = useState({
    nomeCliente: "",
    cep: "",
    rua: "",
    numero: "",
    complemento: "",
    dataEntrega: "",
    valorKm: "",
    distancia: "",
    valorCompra: "",
    distanciaCalculada: false,
  });

  const [mapaKey, setMapaKey] = useState(0);

  useEffect(() => {
    const auth = getAuth();

    const getJwtToken = async () => {
      try {
        const user = auth.currentUser;
        const idToken = await user.getIdToken();
        setJwtToken(idToken);
      } catch (error) {
        console.error("Erro ao obter o token JWT", error);
      }
    };

    onAuthStateChanged(auth, (user) => {
      if (user) {
        getJwtToken();
      }
    });
  }, []);

  const handleChange = (e) => {
    setDadosEnvio({ ...dadosEnvio, [e.target.name]: e.target.value });
    setMapaKey((prevKey) => prevKey + 1);
  };

  const handleBuscarCEP = async () => {
    try {
      const response = await api.get(
        `https://viacep.com.br/ws/${dadosEnvio.cep}/json/`
      );

      if (response.data.erro) {
        console.error("CEP não encontrado");
      } else {
        const { logradouro, bairro, localidade, uf } = response.data;

        setDadosEnvio({
          ...dadosEnvio,
          rua: logradouro,
          bairro,
          cidade: localidade,
          estado: uf,
        });

        setMapaKey((prevKey) => prevKey + 1);
      }
    } catch (error) {
      console.error("Erro ao buscar CEP", error);
    }
  };


  const handleCalcularDistancia = async () => {
    try {
      if (dadosEnvio.cep) {
        await handleBuscarCEP();

        if (dadosEnvio.distancia && dadosEnvio.distancia.endsWith(" km")) {
          const distanciaNumerica = parseInt(dadosEnvio.distancia);
          setDadosEnvio((prevDadosEnvio) => ({
            ...prevDadosEnvio,
            distancia: distanciaNumerica,
            distanciaCalculada: true,
          }));
        } else {
          console.error("Erro ao calcular a distância.");
        }
      }
    } catch (error) {
      console.error("Erro ao calcular a distância:", error);
    }
  };

  const handleEnviarDadosAPI = async () => {
    try {
      if (dadosEnvio.rua || dadosEnvio.cep) {
        const headers = {
          Authorization: `Bearer ${jwtToken}`,
        };

        const distanciaInteira = parseInt(dadosEnvio.distancia);

        const response = await api.post(
          "https://localhost:7192/fretes/calcular",
          { ...dadosEnvio, distancia: distanciaInteira },
          { headers }
        );

        console.log("Resposta da API:", response.data);

        // Limpar campos após o envio bem-sucedido
        if (response.status === 200) {
          setDadosEnvio({
            nomeCliente: "",
            cep: "",
            rua: "",
            numero: "",
            complemento: "",
            dataEntrega: "",
            valorKm: "",
            distancia: "",
            valorCompra: "",
            distanciaCalculada: false,
          });
        }
      } else {
        console.log(
          "Endereço ou CEP não preenchidos. Não é possível calcular."
        );
      }
    } catch (error) {
      console.error("Erro ao enviar dados para a API", error);
    }
  };

  return (
    <div className="principal-container">
      <div className="body-container">
        <div className="corpo">
          <div className="input-container">
            <input
              type="text"
              placeholder="Nome do(a) cliente"
              name="nomeCliente"
              value={dadosEnvio.nomeCliente}
              onChange={handleChange}
            />
          </div>
          <div className="input-cep">
            <input
              type="text"
              placeholder="CEP"
              name="cep"
              value={dadosEnvio.cep}
              onChange={handleChange}
            />
            <button onClick={handleCalcularDistancia} className="button">
              CEP
            </button>
          </div>
          <div className="input-container">
            <input
              type="text"
              placeholder="Cidade"
              name="cidade"
              value={dadosEnvio.cidade}
              onChange={handleChange}
            />
          </div>

          <div className="input-container">
            <input
              type="text"
              placeholder="Endereço"
              name="rua"
              value={dadosEnvio.rua}
              onChange={handleChange}
            />
          </div>
          <div className="input-container">
            <input
              type="text"
              placeholder="Numero"
              name="numero"
              value={dadosEnvio.numero}
              onChange={handleChange}
            />
            <input
              type="text"
              placeholder="Complemento"
              name="complemento"
              value={dadosEnvio.complemento}
              onChange={handleChange}
            />
          </div>
          <div className="input-container">
            <input
              type="date"
              placeholder="Data de Entrega"
              name="dataEntrega"
              value={dadosEnvio.dataEntrega}
              onChange={handleChange}
            />
          </div>
          <div className="input-container">
            <input
              type="text"
              placeholder="Valor por KM"
              name="valorKm"
              value={dadosEnvio.valorKm}
              onChange={handleChange}
            />
          </div>
          <div className="input-container">
            <input
              type="text"
              placeholder="Distância"
              name="distancia"
              value={dadosEnvio.distancia}
              readOnly
            />
          </div>
          <div className="input-container">
            <input
              type="text"
              placeholder="Valor Total da NF"
              name="valorCompra"
              value={dadosEnvio.valorCompra}
              onChange={handleChange}
            />
          </div>
          <div className="buttom-calc">
            <button onClick={handleEnviarDadosAPI} className="button">
              Enviar dados
            </button>
          </div>
        </div>
      </div>
      <Mapa
        key={mapaKey}
        dadosEnvio={dadosEnvio}
        setDadosEnvio={setDadosEnvio}
      />
    </div>
  );
};

export default Calculadora;
