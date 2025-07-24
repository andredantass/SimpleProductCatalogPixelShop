Siga os passos para rodar a aplicação. 
A API foi desenvolvida no Visual Studio 2022 

Pre-requisito
______________________________________________________________

Instalar o Postman na máquina que sera executada a api

Importar Regression Test do Postman
______________________________________________________________

Para fazer o papel do client consumindo os endpoints da api eu utilizei o Postman. E criei um environment de test com chamadas para os endpoints da API.

O arquivo do Postman esta dentro do diretorio SimpleProductCatalog\PostmanRegressionTest.
Este arquivo devera ser importado para o Postman local, seguindo os seguintes passos.

1 - Abrir Postman
2 - Clicar no botão Importar
3 - Selecionar o arquivo PixelShop - SimpleProductAPI.postman_collection que esta na pasta SimpleProductCatalog\PostmanRegressionTest

Todas as chamadas para os endpoints da API estarâo disponiveis no postman, não sendo necessário ficar executando os endpoints pelo Swagger. Assim, o processo de validação a API será muito mais
rápido e dinâmico.

Executando Projeto para teste
______________________________________________________________

1 - Executar a Aplicacao no Visual Studio irá abrir o Swagger (documentação API)

2 - Assim que for importado o RegressionTest do Postman, por favor Rodar os Requests do Postman na seguinte ordem:

	1 - CreateCategory
	2 - GetAllCategories
	3 - UpdateCategory
	4 - GetAllCategory
	5 - CreateProduct
	6 - GetAllProducts
	7 - UpdateProduct
	8 - GetAllProduct
	9 - DeleteProduct
	10 - GetAllProduct
	11 - DeleteCategory
	12 - GetAllCategory

Obs: Os testes do Postman já estão setados para pegar os ids de cada entidade e passar para o próximo
request que precisa desse dado. Por exemplo: ao criar um categoria o id dessa categoria será necessario para criar um Produto. O id categoria será armazenado em um váriavel global, que posteriormente será utilizado pelo request de Criar Produto.