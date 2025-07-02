# Unity 3D Top-Down Action RPG - Projeto de Workshop Avançado

Bem-vindo ao repositório do nosso projeto de workshop de desenvolvimento 3D! Este projeto evoluiu de um protótipo básico para uma pequena experiência de jogo completa, servindo como um estudo de caso prático para uma variedade de técnicas e ferramentas profissionais usadas na indústria de jogos.

O objetivo principal deste projeto é demonstrar um fluxo de trabalho de produção realista, desde a configuração inicial do versionamento até a implementação de mecânicas de gameplay, IA, e polimento visual.

## 🎮 Visão Geral do Jogo

Este é um jogo de ação top-down onde o jogador controla um personagem em um ambiente 3D, enfrenta diferentes tipos de inimigos, coleta itens e tenta sobreviver.

### ✨ Funcionalidades Implementadas

*   **Controle de Jogador Avançado:** Movimento relativo à câmera (estilo "Banjo-Kazooie") e rotação para mirar com o mouse.
*   **Sistema de Combate:** Ataques à distância com projéteis, sistema de munição e feedback de impacto com knockback.
*   **IA Inimiga Variada:**
    *   **Inimigo Perseguidor (`Chaser`):** Persegue o jogador e causa dano por toque.
    *   **Inimigo Atirador (`Shooter`):** Mantém distância e dispara projéteis contra o jogador.
*   **Sistema de Patrulha:** Inimigos patrulham por pontos predefinidos quando não estão em combate.
*   **Sistema de Vida Genérico:** Um componente de vida reutilizável com feedback visual (pisca em vermelho) para jogador e inimigos.
*   **Interface de Usuário (HUD):** Exibição em tempo real da vida do jogador (com `Slider`), contagem de inimigos eliminados e munição.
*   **UI de Mundo (World Space):** Barras de vida que flutuam sobre a cabeça dos inimigos.
*   **Ciclo de Jogo Completo:** Tela de "Game Over" com opção de reiniciar a partida.

---

## 🛠️ Tecnologias e Conceitos Aplicados

Este projeto é um showcase de várias ferramentas e conceitos fundamentais e avançados da Unity:

*   **Controle de Versão Profissional:**
    *   **Git & GitHub:** Configuração do projeto desde o "Commit Zero", com um `.gitignore` específico para Unity, seguindo um fluxo de trabalho de commits incrementais.

*   **Prototipagem e Level Design:**
    *   **ProBuilder:** Utilizado para modelar e construir todo o layout do nível (paredes, rampas, obstáculos) diretamente dentro da Unity.
    *   **ProGrids:** Usado para garantir o alinhamento e a precisão na construção do cenário.

*   **Inteligência Artificial:**
    *   **Unity AI Navigation (NavMesh):** Implementado para dar aos inimigos a capacidade de navegar de forma inteligente pelo cenário, desviando de obstáculos para perseguir o jogador.

*   **Câmera e Polimento Visual:**
    *   **Cinemachine:** Uso da `FreeLook Camera` para criar um sistema de câmera orbital suave e profissional que segue o jogador, controlado pelo mouse.
    *   **Line Renderer & Raycasting:** Implementação de uma "mira a laser" que fornece feedback visual em tempo real, mudando de cor ao mirar em inimigos.
    *   **Animator:** Animações de "Idle/Breathing" para o jogador e animações de estado para os inimigos, controladas via script.

*   **"Game Feel" e Animação por Código:**
    *   **DOTween (Asset de Terceiros):** Utilizado para criar o movimento de "salto" (hop) dos inimigos, demonstrando como bibliotecas de "tweening" podem adicionar "juice" e personalidade ao movimento.

*   **Arquitetura de Software:**
    *   **Padrão Singleton:** Implementado no `GameManager` e `UIManager` para criar pontos de acesso globais e centralizados, organizando o estado do jogo.
    *   **Herança e Polimorfismo:** Criação de uma classe base `Enemy` abstrata e classes filhas (`ChaserEnemy`, `ShooterEnemy`) para gerenciar comportamentos distintos de forma limpa e escalável.
    *   **Princípio da Responsabilidade Única:** Divisão da lógica em componentes focados (`PlayerMovement`, `PlayerAttack`, `HealthSystem`, etc.).

---

## 🚀 Como Começar

Siga estes passos para configurar o projeto em sua máquina local.

### Pré-requisitos
*   **Unity Hub** e uma versão do **Unity Editor 2021.3 (LTS)** ou mais recente.
*   **Git** instalado em sua máquina.
*   Um cliente visual para Git como o **GitHub Desktop** (recomendado).

### Passos de Configuração

1.  **Clone o Repositório:**
    *   Abra o GitHub Desktop e vá em `File > Clone Repository...`.
    *   Selecione a aba `URL`, cole a URL deste repositório e escolha um local em seu computador.

2.  **Importe o DOTween:**
    *   Antes de abrir o projeto na Unity, baixe o **DOTween (HOTween v2)** da Unity Asset Store. Você pode fazer isso através do navegador ou da aba `My Assets` no Package Manager da Unity.
    *   *Nota: Se você abrir o projeto antes, receberá erros de compilação. Basta importar o DOTween e eles serão resolvidos.*

3.  **Abra o Projeto na Unity:**
    *   Abra o **Unity Hub**, clique em "**Open**" e navegue até a pasta que você acabou de clonar.
    *   A Unity levará alguns minutos para importar tudo. Após a importação, se a janela do DOTween aparecer, clique em "**Setup DOTween**".

4.  **Explore a Cena:**
    *   Na janela `Project`, navegue até a pasta `Scenes` e abra a cena principal.
    *   Pressione "Play" e explore todas as funcionalidades implementadas!

    ###SENAI
