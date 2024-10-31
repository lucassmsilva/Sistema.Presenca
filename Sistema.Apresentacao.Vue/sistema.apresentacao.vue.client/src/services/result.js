class Result {
    constructor(data, error, status) {
        this.data = data;
        this.error = error;
        this.status = status;
        this.isSuccess = !error && this.isSuccessStatus(status);
    }

    isSuccessStatus(status) {
        return status >= 200 && status < 300;
    }

    static fromAxiosResponse(response) {
        if (!response || !response.data) {
            return this.failure('Estrutura de resposta inválida', response?.status);
        }
        return this.success(response.data, response.status);
    }

    static fromAxiosError(error) {
        let errorMessage;
        let status;

        if (error.response) {
            status = error.response.status;
            
            // Handle specific status codes
            switch (status) {
                case 400:
                    errorMessage = error.response.data?.Message || 'Requisição inválida';
                    break;
                case 401:
                    errorMessage = 'Não autorizado';
                    break;
                case 403:
                    errorMessage = 'Acesso negado';
                    break;
                case 404:
                    errorMessage = 'Recurso não encontrado';
                    break;
                case 422:
                    errorMessage = error.response.data?.Message || 'Dados inválidos';
                    break;
                case 500:
                    errorMessage = 'Erro interno do servidor';
                    break;
                case 503:
                    errorMessage = 'Serviço indisponível';
                    break;
                default:
                    errorMessage = error.response.data?.Message || 
                        `Erro ${status}: ${error.response.statusText}`;
            }
        } else if (error.request) {
            status = 0; // No response received
            errorMessage = 'Não obteve resposta do servidor.';
        } else {
            status = -1; // Request setup error
            errorMessage = error.message || 'Um erro inesperado ocorreu';
        }

        return new Result(null, errorMessage, status);
    }
    // Helper methods
    static success(data, status = 200) {
        return new Result(data, null, status);
    }

    static failure(error, status = 500) {
        return new Result(null, error, status);
    }
}

export default Result;