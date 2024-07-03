import React, { useEffect } from "react";
import "./map.css";

let map;
let directionsService;
let directionsRenderer;

const MapComponent = ({ dadosEnvio, setDadosEnvio }) => {
  useEffect(() => {
    const initMap = () => {
      map = new window.google.maps.Map(document.getElementById("map"), {
        center: { lat: -19.7621, lng: -44.0844 },
        zoom: 10,
      });

      directionsService = new window.google.maps.DirectionsService();
      directionsRenderer = new window.google.maps.DirectionsRenderer();
      directionsRenderer.setMap(map);

      calcularDistancia();
    };

    const calcularDistancia = () => {
      if (
        !dadosEnvio.distancia &&
        dadosEnvio.cep &&
        !dadosEnvio.distanciaCalculada
      ) {
        const origem =
          "Av Dionísio Gomes n 67, bairro Veneza - Ribeirão das Neves";
        const destino = `${dadosEnvio.rua}, ${dadosEnvio.numero}, ${dadosEnvio.bairro}, ${dadosEnvio.cidade}, ${dadosEnvio.estado}`;

        const request = {
          origin: origem,
          destination: destino,
          travelMode: "DRIVING",
        };

        directionsService.route(request, (result, status) => {
          if (status === "OK") {
            const distancia = result.routes[0].legs[0].distance.text;
            console.log("Distância:", distancia);

            setDadosEnvio((prevDadosEnvio) => ({
              ...prevDadosEnvio,
              distancia: distancia,
              distanciaCalculada: true,
            }));
          } else {
            console.error("Erro ao calcular a distância:", status);
          }
        });
      }
    };

    const script = document.createElement("script");
    script.src = `https://maps.googleapis.com/maps/api/js?key=AIzaSyDGHMmoPL2pV5s0f723u2p9v5N2aKeIiHY&callback=initMap`;
    script.async = true;
    script.defer = true;
    script.onload = initMap;

    document.head.appendChild(script);

    return () => {
      document.head.removeChild(script);
    };
  }, [dadosEnvio, setDadosEnvio]);

  return (
    <div className="map-container">
      <div id="map"></div>
    </div>
  );
};

export default MapComponent;
