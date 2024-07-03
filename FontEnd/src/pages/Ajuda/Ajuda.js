// Ajuda.js
import React, { useState, useEffect } from "react";
import "../Ajuda/Ajuda.css";
import Footer from "../../components/Footer";
import Menu from "../../components/Menu";
import Header from "../../components/Header";
import Question from "../../components/Question";

const dataCollections = [
  {
    questao: "CEP e Endereço:",
    resposta:
      "Insira o CEP do seu endereço de entrega no campo designado e clique em 'Buscar'. Seu endereço será atualizado automaticamente, e a rota correspondente será destacada no mapa localizado na parte inferior da tela.",
  },
  {
    questao: "Detalhes Opcionais:",
    resposta:
      "Complete os campos 'Número' e 'Complemento' conforme necessário. Estes são opcionais, mas fornecem detalhes adicionais para uma entrega precisa.",
  },
  {
    questao: "Volume de Tintas:",
    resposta:
      "Especifique o volume aproximado de tintas que você planeja enviar. Isso permitirá que nosso sistema calcule o percentual de ocupação ideal para otimizar sua entrega.",
  },
  {
    questao: "Cálculo do Frete:",
    resposta:
      "Ao clicar em 'Calcular', visualize instantaneamente o valor estimado do frete. Além disso, tenha a opção de salvar esse cálculo, inserindo o nome do cliente e a data de entrega desejada para referência futura.",
  },
];

const Ajuda = () => {
  const [accordion, setAccordion] = useState(null);

  const toggleAccordion = (index) => {
    setAccordion((prevAccordion) => (prevAccordion === index ? null : index));
  };

  const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => {
    setMenuOpen((prevMenuOpen) => !prevMenuOpen);
  };

  useEffect(() => {
    document.body.classList.add("ajuda-page-body");

    return () => {
      document.body.classList.remove("ajuda-page-body");
    };
  }, []);

  return (
    <>
      <div className="container_duvidas">
        <Header isOpen={menuOpen} onToggleMenu={toggleMenu} />
        <Menu isOpen={menuOpen} onClose={toggleMenu} />
        <div>
          <h1 className="text_question">Perguntas Frequentes</h1>
          <h1 className="text_resposta">Vamos responder algumas de suas perguntas</h1>
        </div>
        <div className="accordion__faq">
          {dataCollections.map((item, index) => (
            <Question
              key={index}
              index={index}
              accordion={accordion}
              toggleAccordion={toggleAccordion}
              {...item}
            />
          ))}
        </div>
      </div>
      <Footer />
    </>
  );
};

export default Ajuda;
