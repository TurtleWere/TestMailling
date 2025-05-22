        async function Mailling() {
            const emailData = {
                from: document.getElementById('from').value,
                to: document.getElementById('to').value,
                copy: document.getElementById('copy').value,
                blindCopy: document.getElementById('blindCopy').value,
                title: document.getElementById('title').value,
                body: document.getElementById('body').value
            };
           
            const isValidEmail = (email) => 
            {
                const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                return regex.test(email);
            };
 
            const validateEmailList = (emails) => 
            {
                if (!emails.trim()) return true; 
        
                const emailList = emails.split(';').map(email => email.trim());
                return emailList.every(email => isValidEmail(email));
            };

            if (!emailData.to || !validateEmailList(emailData.to)) 
            {
                alert('Неверный формат');
                return;
            }

            if (emailData.copy && !validateEmailList(emailData.copy)) 
            {
                alert('Неверный формат');
                return;
            }

            try {
                const response = await fetch('https://localhost:7187/api/Mail/mail', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(emailData)
                });

                if (!response.ok) {
                    const errorText = await response.text();
                    throw new Error(`Server error (${response.status}): ${errorText}`);
                }

                const result = await response.json();
                displayResult(result);
            } catch (error) {
                console.error('Error:', error);
                alert(error.message);
            }
        }

        function displayResult(result) {
            document.getElementById('result-from').textContent = result.from || '-';
            document.getElementById('result-to').textContent = result.to || '-';
            document.getElementById('result-copy').textContent = result.copy || '-';
            document.getElementById('result-blindCopy').textContent = result.blindCopy || '-';
            document.getElementById('result-title').textContent = result.title || '-';
            document.getElementById('result-body').textContent = result.message || '-';
            
            document.getElementById('result').style.display = 'block';
        }