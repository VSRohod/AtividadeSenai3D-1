# Unity 3D Top-Down Action Game - Projeto Base para Workshop

Bem-vindo ao projeto base para o nosso workshop de desenvolvimento 3D na Unity! Este repositório contém um ponto de partida simples para um jogo de ação top-down, permitindo que foquemos em adicionar funcionalidades avançadas em vez de começar do zero.

## 🎯 Objetivo do Projeto

O objetivo deste projeto é servir como base para aprender e implementar os seguintes sistemas em uma sessão prática:
*   **Level Design** com ProBuilder.
*   **Inteligência Artificial** de inimigos com o sistema NavMesh.
*   **Câmeras Inteligentes** com Cinemachine.
*   **Interface de Usuário (UI)** básica para feedback ao jogador.
*   **Fluxo de Trabalho Profissional** com Git e GitHub.

## ✨ Funcionalidades Inclusas no Projeto Base

*   **Jogador:** Um `GameObject` com um `CharacterController` ou `Rigidbody` para movimentação.
*   **Movimentação:** Script de controle top-down básico (WASD).
*   **Ataque:** Script de ataque à distância que instancia um projétil.
*   **Assets:** Prefabs e materiais simples para o Jogador, Inimigo, Projétil e um Item Coletável.

## 🛠️ Como Começar (Instruções para Alunos)

Siga estes passos para configurar o projeto em sua máquina local.

### Pré-requisitos
*   **Unity Hub** e uma versão do **Unity Editor 2021.3 (LTS)** ou mais recente.
*   **Git** instalado em sua máquina. (https://git-scm.com/)
*   Um cliente visual para Git como o **GitHub Desktop** (recomendado para iniciantes). (https://desktop.github.com/)
*   Uma conta no **GitHub**.

### Passos de Configuração

1.  **Clone o Repositório:**
    *   Clique no botão verde "**<> Code**" no topo desta página.
    *   Copie a URL (HTTPS).
    *   Abra o GitHub Desktop e vá em `File > Clone Repository...`. Cole a URL e escolha um local em seu computador para salvar o projeto.

2.  **Abra o Projeto na Unity:**
    *   Abra o **Unity Hub**.
    *   Clique em "**Open**" e navegue até a pasta que você acabou de clonar.
    *   A Unity levará alguns minutos para importar o projeto e reconstruir a pasta `Library`. Isso é normal!

3.  **Verifique a Cena:**
    *   Na janela `Project`, navegue até a pasta `Scenes`.
    *   Abra a cena principal (ex: `MainScene`). Você deve ver o jogador em um plano vazio.

4.  **Pronto!**
    *   Pressione "Play" para testar as funcionalidades básicas. Agora você está pronto para começar a adicionar as novas features durante o workshop.

## 📚 Conceitos que Serão Abordados

Durante a aula, vamos expandir este projeto base implementando:

*   **ProBuilder:** Para modelar um cenário 3D interessante.
*   **NavMesh:** Para dar vida aos nossos inimigos, permitindo que eles naveguem pelo cenário.
*   **Cinemachine:** Para criar uma câmera dinâmica que segue o jogador de forma suave.
*   **UI com Canvas e TextMeshPro:** Para criar um placar de itens coletados.
*   **Controle de Versão:** Faremos `commits` e `pushes` para cada nova funcionalidade, simulando um ambiente de desenvolvimento real.
