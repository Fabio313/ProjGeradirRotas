<h1>Como utilizar</h1><br><br>
-O projeto foi feito em mongo db então tenha um servidor rodando com a porta padrão para utilização.
<br>
-Antes de começar va dentro do projeto na biblioteca de classes "Model", entre na pasta "Service",
<br>
na classe "LeitorArquivos" alterar o caminho do arquivo xlsx que sera lido.
<br>
-Certifique-se de que o arquivo excel não contem linha residuais, todas as linhas que nao forem utilizadas devem ser apagadas<br>
pois caso o sistema recebe uma linha vazia ira quebrar, assim como qualquer celula não pode ter um conteudo vazio, caso não tenha informação
coloque um "-" ou um espaço vazio(" ").
<br>
<br>
-No momento de cadastro de uma cidade voce escolhe os times que estarão nela,
<br>
assim como no momento de cadastro de equipe voce escolhera as pessoas que estarão nessa equipe
<br>
-Logo deve ser cadastrado na ordem Pessoas => Equipes => Cidades
<br>
-Durante o primeiro cadastro devido a ser a primeira conexão pode demorar para aparecer o que foi cadastrado na tela principal,<br>
por isso recarregue a pagina em questão.<br><br>

<h2>Não feito</h2>
<br>
-O nome das colunas não é dinamicamente feito(feito com base a primeira linha do excel) por isso caso o nome da 
<br>
coluna for mudado ele continuara funcionando porem no frontend mostra-ra o nome antido da coluna
<br>
-Não é possivel escolher quais colunas serão salvas no arquivo, o arquivo vira como padrão com as informações obrigatorias(OS,BASE,SERVIÇO,ENDERECO COMPLETO);
<br>
-Não é possivel alterar as pessoas de um time, para alterar deve-se apagar o time e 
<br>
recriar com as novas pessoas(status das pessoas volta para disponivel quando se deleta o time que pertencem);
